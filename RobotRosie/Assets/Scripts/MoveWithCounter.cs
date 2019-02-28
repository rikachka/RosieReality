using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCounter : MonoBehaviour
{
    const int MAX_POSSIBLE_COUNTERS_NUMBER = 15;
    const float COUNTER_SHIFT_FACTOR = 0.5F;
    const float COUNTER_SHIFT = 1;

    public MoveTile.Type move_type;

    // Number of counters available to the player (both in the panel and in the field).
    public int max_availbale_number;
    // Number of counters available in the panel.
    public int available_number;
    // Draw counters on the right or left from the move tile.
    public bool are_counters_on_the_right;

    public MoveTile move_tile;
    public GameObject init_counter_object;
    Counter[] counter_panel;


    public Move.Direction GetDirection()
    {
        return move_tile.GetDirection();
    }

    public void SetDirection(Move.Direction direction)
    {
        move_tile.SetDirection(direction);
    }


    // Create / Update operations.
    void CreateMoveWithCounter(int x, Counter.Type counter_type)
    {
        Vector3 left_top_coords = transform.position;
        float shift = COUNTER_SHIFT + x * COUNTER_SHIFT_FACTOR; 
        if (!are_counters_on_the_right) shift *= -1;
        Vector3 coords = new Vector3(left_top_coords.x + shift, left_top_coords.y, left_top_coords.z);
        counter_panel[x] = Instantiate(init_counter_object, coords, new Quaternion()).GetComponent<Counter>();
        counter_panel[x].img_type = counter_type;
    }

    void CreateMoveWithCounter()
    {
        move_tile.type = move_type;

        counter_panel = new Counter[MAX_POSSIBLE_COUNTERS_NUMBER];

        for (int x = 0; x < MAX_POSSIBLE_COUNTERS_NUMBER; x++)
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

        UpdateMovesWithCounterOfType(Counter.Type.EMPTY, 0, available_number);
        UpdateMovesWithCounterOfType(Counter.Type.AVAILABLE, available_number, max_availbale_number);
        UpdateMovesWithCounterOfType(Counter.Type.NO_COUNTER, max_availbale_number, MAX_POSSIBLE_COUNTERS_NUMBER);
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