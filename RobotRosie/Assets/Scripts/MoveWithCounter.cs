using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCounter : MonoBehaviour
{
    const int MAX_POSSIBLE_COUNTERS = 15;
    float COUNTER_SHIFT_FACTOR = 0.5F;
    float COUNTER_SHIFT = 1;

    public MoveTile.Type move_type = 0;
    public Move.Direction move_direction = 0;

    public int number_max = 0;
    public int number_available = 0;
    public bool are_counters_on_the_right = true;

    public MoveTile move_tile;
    public GameObject init_counter;
    Counter[] counter_panel;

    void CreateMoveWithCounter(int x, Counter.Type counter_type)
    {
        Vector3 left_top_coords = transform.position;
        float shift = COUNTER_SHIFT + x * COUNTER_SHIFT_FACTOR; 
        if (!are_counters_on_the_right) shift *= -1;
        Vector3 coords = new Vector3(left_top_coords.x + shift, left_top_coords.y, left_top_coords.z);
        counter_panel[x] = Instantiate(init_counter, coords, new Quaternion()).GetComponent<Counter>();
        counter_panel[x].img_type = counter_type;
    }

    void CreateMoveWithCounter()
    {
        move_tile.type = move_type;
        move_tile.SetDirection(move_direction);

        counter_panel = new Counter[MAX_POSSIBLE_COUNTERS];

        for (int x = 0; x < MAX_POSSIBLE_COUNTERS; x++)
        {
            CreateMoveWithCounter(x, Counter.Type.NO_COUNTER);
        }
    }

    void UpdateMovesWithCounterOfType(Counter.Type counter_type, int min_index, int max_index)
    {
        for (int x = min_index; x < max_index; x++)
        {
            counter_panel[x].img_type = counter_type;
        }
    }

    void UpdateMoveWithCounter()
    {
        move_tile.type = move_type;
        move_tile.SetDirection(move_direction);

        UpdateMovesWithCounterOfType(Counter.Type.EMPTY, 0, number_available);
        UpdateMovesWithCounterOfType(Counter.Type.AVAILABLE, number_available, number_max);
        UpdateMovesWithCounterOfType(Counter.Type.NO_COUNTER, number_max, MAX_POSSIBLE_COUNTERS);
    }

    void Start()
    {
        CreateMoveWithCounter();
    }

    void Update()
    {
        UpdateMoveWithCounter();
    }
}