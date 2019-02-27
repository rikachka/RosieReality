using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCounter : MonoBehaviour
{
    public MoveTile.Type move_type = 0;
    public MoveTile.Direction move_direction = 0;

    public int number_max = 0;
    public int number_available = 0;
    public bool are_counters_on_the_right = true;

    public MoveTile move_tile;
    public GameObject init_counter;
    GameObject[] counter_panel;

    float step_factor = 0.5F;
    float shift_main = 1;

    public GameObject GetMoveTile()
    {
        return transform.Find("MoveTile").gameObject;
    }

    void CreateMoveWithCounter(int x, Counter.Type counter_type)
    {
        Vector3 left_top_coords = transform.position;
        float shift = shift_main + x * step_factor; 
        if (!are_counters_on_the_right) shift *= -1;
        Vector3 coords = new Vector3(left_top_coords.x + shift, left_top_coords.y, left_top_coords.z);
        counter_panel[x] = Instantiate(init_counter, coords, new Quaternion());
        counter_panel[x].GetComponent<Counter>().img_type = counter_type;
    }

    void CreateMoveWithCounter()
    {
        move_tile.type = move_type;
        move_tile.direction = move_direction;

        counter_panel = new GameObject[number_max];

        for (int x = 0; x < number_available; x++)
        {
            CreateMoveWithCounter(x, Counter.Type.EMPTY);
        }

        for (int x = number_available; x < number_max; x++)
        {
            CreateMoveWithCounter(x, Counter.Type.AVAILABLE);
        }
    }

    void UpdateMoveWithCounter()
    {
        move_tile.type = move_type;
        move_tile.direction = move_direction;

        for (int x = 0; x < number_available; x++)
        {
            counter_panel[x].GetComponent<Counter>().img_type = Counter.Type.EMPTY;
        }

        for (int x = number_available; x < number_max; x++)
        {
            counter_panel[x].GetComponent<Counter>().img_type = Counter.Type.AVAILABLE;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        move_tile = GetMoveTile().GetComponent<MoveTile>();
        CreateMoveWithCounter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveWithCounter();
    }

    private void OnDestroy()
    {
        foreach (GameObject counter in counter_panel)
        {
            Destroy(counter);
        }
    }
}