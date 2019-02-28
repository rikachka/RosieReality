using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesPanel : MonoBehaviour
{
    const int NO_ACTIVE_MOVE_IN_PANEL = -1;
    const float MOVE_TILES_SHIFT_FACTOR = 1.1F;
    const int MOVES_PANEL_SIZE = 4;

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
    public MoveTile.Type player_tile_type;
    // Draw counters on the right or left from the move tile.
    public bool are_counters_on_the_right;

    public GameObject init_move_with_counter_object;
    MoveWithCounter[] moves_panel;

    // One of the tiles in the panel can be active. This tile will be used 
    // in the field or in the exchanger if we click there.
    int active_in_panel = NO_ACTIVE_MOVE_IN_PANEL;



    // Click from the MoveWithCounter at the index 'y'.
    public void ClickByIndex(int y)
    {
        MoveWithCounter move_tile_clicked = moves_panel[y];

        // If there are no available move tiles, click does not work.
        // DELETE tile is special, it brings tiles back to the panel, thus it does not need counters. 
        if (move_tile_clicked.available_number <= 0 && move_tile_clicked.GetDirection() != Move.Direction.DELETE) return;

        // If there was not active tile before, mark this tile as active and return.
        if (active_in_panel == NO_ACTIVE_MOVE_IN_PANEL)
        {
            move_tile_clicked.move_type = MoveTile.Type.ACTIVE;
            active_in_panel = y;
            return;
        }

        // If there was active tile, remove selection from it.
        MoveWithCounter move_tile_active = moves_panel[active_in_panel];
        if (move_tile_active.GetDirection() == Move.Direction.DELETE)
        {
            move_tile_active.move_type = MoveTile.Type.DELETE;
        }
        else
        {
            move_tile_active.move_type = player_tile_type;
        }

        // If we clicked the tile which was active, do not put selection anywhere.
        // Otherwise make the clicked tile active.
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


    // Return the direction of the active move tile if any. It is taken from 
    // the panel thus it is removed from the available in panel number.
    public Move.Direction TakeActiveMoveTile()
    {
        if (active_in_panel != NO_ACTIVE_MOVE_IN_PANEL)
        {
            MoveWithCounter move_with_counter_active = moves_panel[active_in_panel];
            Move.Direction move_direction = move_with_counter_active.GetDirection();

            if (move_direction != Move.Direction.DELETE)
            {
                move_with_counter_active.available_number--;
                // If there are no more available move tiles of this type,
                // remove the selection of the active tile.
                if (move_with_counter_active.available_number == 0)
                {
                    move_with_counter_active.move_type = player_tile_type;
                    active_in_panel = NO_ACTIVE_MOVE_IN_PANEL;
                }
            }

            return move_direction;
        }
        return Move.Direction.NO_DIRECTION;
    }

    // The move tile is returned to the panel. It is added back to the available in panel number.
    public void ReturnMoveTile(Move.Direction returned_direction)
    {
        foreach (MoveWithCounter move_with_counter in moves_panel)
        {
            if (move_with_counter.GetDirection() == returned_direction)
            {
                move_with_counter.available_number++;
            }
        }
    }

    // Give 'given_direction' to the other player, recieve 'received_direction' from the other player.
    // The number of corresponding tiles increased / decreased (not only in the panel, but in total).
    public void ExchangeMoveTile(Move.Direction received_direction, Move.Direction given_direction)
    {
        foreach (MoveWithCounter move_with_counter in moves_panel)
        {
            if (move_with_counter.GetDirection() == received_direction)
            {
                move_with_counter.available_number++;
                move_with_counter.max_availbale_number++;
            }
            if (move_with_counter.GetDirection() == given_direction)
            {
                // Do not decrease 'move_with_counter.available_number' as it was already given
                // to the exchanger beforehands.
                move_with_counter.max_availbale_number--;
            }
        }
    }


    // Return number of move tiles available in the panel in total.
    public int NumberAvailableMoves()
    {
        int number_available_moves = 0;
        foreach (MoveWithCounter move_with_counter in moves_panel)
        {
            number_available_moves += move_with_counter.available_number;
        }
        return number_available_moves;
    }



    // Create / Update operations.
    void CreateMoveTile(int y)
    {
        Vector3 left_top_coords = transform.position;
        Vector3 coords = new Vector3(left_top_coords.x, left_top_coords.y - y * MOVE_TILES_SHIFT_FACTOR, left_top_coords.z);
        moves_panel[y] = Instantiate(init_move_with_counter_object, coords, new Quaternion()).GetComponent<MoveWithCounter>();

        MoveWithCounter move_with_counter = moves_panel[y];
        move_with_counter.are_counters_on_the_right = are_counters_on_the_right;

        move_with_counter.move_tile.GetComponent<MoveTileClick>().parent = this.gameObject;
        move_with_counter.move_tile.GetComponent<MoveTileClick>().parent_index = y;
    }

    public void CreateMovesPanel()
    {
        moves_panel = new MoveWithCounter[MOVES_PANEL_SIZE];

        for (int y = 0; y < MOVES_PANEL_SIZE; y++)
        {
            CreateMoveTile(y);
        }
    }


    void UpdateMoveTile(int y, MoveWithCounterInfo move_tile_info, MoveTile.Type move_tile_type)
    {
        MoveWithCounter move_with_counter = moves_panel[y];
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
            UpdateMoveTile(y, moves_info[y], player_tile_type);
        }

        UpdateMoveTile(moves_types_number, new MoveWithCounterInfo(Move.Direction.DELETE, 0, 0), MoveTile.Type.DELETE);
    }

    void Start()
    {
        //CreateMovesPanel();
    }
}
