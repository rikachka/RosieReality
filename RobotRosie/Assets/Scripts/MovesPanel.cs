using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesPanel : MonoBehaviour
{
    struct MovesInfo
    {
        public MoveDirection move_direction;
        public int number_max; 
        public int number_available;

        public MovesInfo(MoveDirection move_direction_, int number_max_, int number_available_)
        {
            move_direction = move_direction_;
            number_max = number_max_;
            number_available = number_available_;
        }
    }

    public enum MoveDirection { FORWARD, LEFT, RIGHT }

    public GameObject init_move_with_counter;
    public int player;

    GameObject[] panel;

    const int NO_ACTIVE = -1;
    const int ACTIVE_MOVE = 0;
    int active_in_panel = NO_ACTIVE;

    MovesInfo[] moves_info =
    {
        new MovesInfo(MoveDirection.FORWARD, 5, 3),
        new MovesInfo(MoveDirection.LEFT, 6, 2),
        new MovesInfo(MoveDirection.RIGHT, 4, 4)
    };

    int size = 3;
    float step_factor = 1.1F;

    void CreateMovesPanel()
    {
        Vector3 left_top_coords = transform.position;
        panel = new GameObject[size];

        for (int y = 0; y < size; y++)
        {
            Vector3 coords = new Vector3(left_top_coords.x, left_top_coords.y - y * step_factor, left_top_coords.z);
            panel[y] = Instantiate(init_move_with_counter, coords, new Quaternion());

            MoveWithCounter move_with_counter = panel[y].GetComponent<MoveWithCounter>();
            move_with_counter.move_type = player;
            move_with_counter.move_direction = (int)moves_info[y].move_direction;
            move_with_counter.number_max = moves_info[y].number_max;
            move_with_counter.number_available = moves_info[y].number_available;

            move_with_counter.GetMoveTile().GetComponent<MoveTileClick>().parent_panel = this.gameObject;
            move_with_counter.GetMoveTile().GetComponent<MoveTileClick>().coord_in_panel = y;
        }
    }

    public void Click(int y)
    {
        if (active_in_panel == NO_ACTIVE)
        {
            active_in_panel = y;
            panel[active_in_panel].GetComponent<MoveWithCounter>().move_type = ACTIVE_MOVE;
        }
        else
        {
            panel[active_in_panel].GetComponent<MoveWithCounter>().move_type = player;
            if (y == active_in_panel)
            {
                active_in_panel = NO_ACTIVE;
            }
            else
            {
                active_in_panel = y;
                panel[active_in_panel].GetComponent<MoveWithCounter>().move_type = ACTIVE_MOVE;
            }
        }
    }

    public void TakeActiveMoveTile()
    {
        if (active_in_panel != NO_ACTIVE)
        {
            panel[active_in_panel].GetComponent<MoveWithCounter>().number_available--;
            active_in_panel = NO_ACTIVE;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateMovesPanel();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
