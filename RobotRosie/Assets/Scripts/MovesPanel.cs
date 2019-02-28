using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesPanel : MonoBehaviour
{
    const int NO_ACTIVE_MOVE_IN_PANEL = -1;
    const float MOVES_IN_PANEL_SHIFT_FACTOR = 1.1F;
    const int MOVES_IN_PANEL_NUMBER = 4;

    public struct MoveWithCounterInfo
    {
        public Move.Direction move_direction;
        public int max_available_number; 
        public int available_number;

        public MoveWithCounterInfo(Move.Direction move_direction_, int max_available_number_, int available_number_)
        {
            move_direction = move_direction_;
            max_available_number = max_available_number_;
            available_number = available_number_;
        }
    }

    // Player1 or Player2.
    public MoveTile.Type default_move_tile_type;
    // Draw counters on the right or left from the move tile.
    public bool are_counters_on_the_right;

    public GameObject init_move_with_counter_object;
    GameObject[] panel = { };

    // One of the tiles in the panel can be active. This tile will be used 
    // in the field or in the exchanger if we click there.
    int active_in_panel = NO_ACTIVE_MOVE_IN_PANEL;



    public void ClickByIndex(int y)
    {
        MoveWithCounter move_tile_clicked = panel[y].GetComponent<MoveWithCounter>();

        if (move_tile_clicked.available_number <= 0 && move_tile_clicked.GetDirection() != Move.Direction.DELETE)
        {
            return;
        }

        if (active_in_panel == NO_ACTIVE_MOVE_IN_PANEL)
        {
            move_tile_clicked.move_type = MoveTile.Type.ACTIVE;
            active_in_panel = y;
            return;
        }

        MoveWithCounter move_tile_active = panel[active_in_panel].GetComponent<MoveWithCounter>();

        if (move_tile_active.GetDirection() == Move.Direction.DELETE)
        {
            move_tile_active.move_type = MoveTile.Type.DELETE;
        }
        else
        {
            move_tile_active.move_type = default_move_tile_type;
        }

        if (y == active_in_panel)
        {
            active_in_panel = NO_ACTIVE_MOVE_IN_PANEL;
        }
        else
        {
            move_tile_clicked.move_type = MoveTile.Type.ACTIVE;
            active_in_panel = y;
        }
    }

    public Move.Direction TakeActiveMoveTile()
    {
        if (active_in_panel != NO_ACTIVE_MOVE_IN_PANEL)
        {
            MoveWithCounter move_with_counter = panel[active_in_panel].GetComponent<MoveWithCounter>();
            Move.Direction move_direction = move_with_counter.GetDirection();

            if (move_direction != Move.Direction.DELETE)
            {
                move_with_counter.available_number--;
                if (move_with_counter.available_number == 0)
                {
                    move_with_counter.move_type = default_move_tile_type;
                    active_in_panel = NO_ACTIVE_MOVE_IN_PANEL;
                }
            }

            return move_direction;
        }
        return Move.Direction.NO_DIRECTION;
    }

    public void ReturnMoveTile(Move.Direction returned_direction)
    {
        foreach (GameObject panel_elem in panel)
        {
            MoveWithCounter move_with_counter = panel_elem.GetComponent<MoveWithCounter>();
            if (move_with_counter.GetDirection() == returned_direction)
            {
                move_with_counter.available_number++;
                return;
            }
        }
    }

    public void ExchangeMoveTile(Move.Direction received_direction, Move.Direction given_direction)
    {
        foreach (GameObject panel_elem in panel)
        {
            MoveWithCounter move_with_counter = panel_elem.GetComponent<MoveWithCounter>();
            if (move_with_counter.GetDirection() == received_direction)
            {
                move_with_counter.available_number++;
                move_with_counter.max_availbale_number++;
            }
            if (move_with_counter.GetDirection() == given_direction)
            {
                move_with_counter.max_availbale_number--;
            }
        }
    }

    public int NumberAvailableMoves()
    {
        int number_available_moves = 0;
        foreach (GameObject panel_elem in panel)
        {
            number_available_moves += panel_elem.GetComponent<MoveWithCounter>().available_number;
        }
        return number_available_moves;
    }


    void UpdateMoveTile(int y, MoveWithCounterInfo move_tile_info, MoveTile.Type move_tile_type)
    {
        MoveWithCounter move_with_counter = panel[y].GetComponent<MoveWithCounter>();
        move_with_counter.move_type = move_tile_type;
        move_with_counter.are_counters_on_the_right = are_counters_on_the_right;
        move_with_counter.SetDirection(move_tile_info.move_direction);
        move_with_counter.max_availbale_number = move_tile_info.max_available_number;
        move_with_counter.available_number = move_tile_info.available_number;
    }

    public void UpdateMovesPanel(MoveWithCounterInfo[] moves_info)
    {
        int moves_types_number = moves_info.Length;

        for (int y = 0; y < moves_types_number; y++)
        {
            UpdateMoveTile(y, moves_info[y], default_move_tile_type);
        }

        UpdateMoveTile(moves_types_number, new MoveWithCounterInfo(Move.Direction.DELETE, 0, 0), MoveTile.Type.DELETE);
    }


    void CreateMoveTile(int y)
    {
        Vector3 left_top_coords = transform.position;
        Vector3 coords = new Vector3(left_top_coords.x, left_top_coords.y - y * MOVES_IN_PANEL_SHIFT_FACTOR, left_top_coords.z);
        panel[y] = Instantiate(init_move_with_counter_object, coords, new Quaternion());

        MoveWithCounter move_with_counter = panel[y].GetComponent<MoveWithCounter>();
        move_with_counter.are_counters_on_the_right = are_counters_on_the_right;

        move_with_counter.move_tile.GetComponent<MoveTileClick>().parent = this.gameObject;
        move_with_counter.move_tile.GetComponent<MoveTileClick>().parent_index = y;
    }

    void CreateMovesPanel()
    {
        panel = new GameObject[MOVES_IN_PANEL_NUMBER];

        for (int y = 0; y < MOVES_IN_PANEL_NUMBER; y++)
        {
            CreateMoveTile(y);
        }
    }

    void Start()
    {
        CreateMovesPanel();
    }
}
