using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCounter : MonoBehaviour
{
    public enum CounterType { EMPTY, AVAILABLE }

    public int move_type = 0;
    public int move_direction = 0;

    public int number_max = 0;
    public int number_available = 0;

    public MoveTile move_tile;
    public GameObject counter;
    GameObject[] counter_panel;

    float step_factor = 0.5F;
    float shift = 1;

    void CreateMoveWithCounter()
    {
        move_tile.type = move_type;
        move_tile.direction = move_direction;

        Vector3 left_top_coords = transform.position;
        counter_panel = new GameObject[number_max];

        for (int x = 0; x < number_available; x++)
        {
            counter_panel[x] = Instantiate(counter);
            counter_panel[x].GetComponent<Counter>().img_type = (int)CounterType.EMPTY;
            counter_panel[x].transform.position = new Vector3(left_top_coords.x + shift + x * step_factor, left_top_coords.y, left_top_coords.z);
        }

        for (int x = number_available; x < number_max; x++)
        {
            counter_panel[x] = Instantiate(counter);
            counter_panel[x].GetComponent<Counter>().img_type = (int)CounterType.AVAILABLE;
            counter_panel[x].transform.position = new Vector3(left_top_coords.x + shift + x * step_factor, left_top_coords.y, left_top_coords.z);
        }
    }

    void UpdateMoveWithCounter()
    {
        move_tile.type = move_type;
        move_tile.direction = move_direction;

        for (int x = 0; x < number_available; x++)
        {
            counter_panel[x].GetComponent<Counter>().img_type = (int)CounterType.EMPTY;
        }

        for (int x = number_available; x < number_max; x++)
        {
            counter_panel[x].GetComponent<Counter>().img_type = (int)CounterType.AVAILABLE;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        move_tile = transform.Find("MoveTile").gameObject.GetComponent<MoveTile>();
        CreateMoveWithCounter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveWithCounter();
    }
}
