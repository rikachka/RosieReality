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

    public GameObject move_with_counter;
    public int player;

    GameObject[] panel;

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
            panel[y] = Instantiate(move_with_counter);
            panel[y].GetComponent<MoveWithCounter>().move_type = player;
            panel[y].GetComponent<MoveWithCounter>().move_direction = (int)moves_info[y].move_direction;
            panel[y].GetComponent<MoveWithCounter>().number_max = moves_info[y].number_max;
            panel[y].GetComponent<MoveWithCounter>().number_available = moves_info[y].number_available;
            panel[y].transform.position = new Vector3(left_top_coords.x, left_top_coords.y - y * step_factor, left_top_coords.z);
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
