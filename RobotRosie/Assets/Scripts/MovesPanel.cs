﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesPanel : MonoBehaviour
{
    const int NO_ACTIVE = -1;

    public struct MoveInfo
    {
        public MoveTile.Direction move_direction;
        public int number_max; 
        public int number_available;

        public MoveInfo(MoveTile.Direction move_direction_, int number_max_, int number_available_)
        {
            move_direction = move_direction_;
            number_max = number_max_;
            number_available = number_available_;
        }
    }

    public GameObject init_move_with_counter;
    public MoveTile.Type default_move_tile_type;
    public bool are_counters_on_the_right;

    GameObject[] panel = { };

    int active_in_panel = NO_ACTIVE;

    int size = 4;
    float step_factor = 1.1F;

    void CreateMoveTile(int y, MoveInfo move_tile_info, MoveTile.Type move_tile_type) 
    {
        Vector3 left_top_coords = transform.position;
        Vector3 coords = new Vector3(left_top_coords.x, left_top_coords.y - y * step_factor, left_top_coords.z);
        panel[y] = Instantiate(init_move_with_counter, coords, new Quaternion());

        MoveWithCounter move_with_counter = panel[y].GetComponent<MoveWithCounter>();
        move_with_counter.move_type = move_tile_type;
        move_with_counter.are_counters_on_the_right = are_counters_on_the_right;
        move_with_counter.move_direction = move_tile_info.move_direction;
        move_with_counter.number_max = move_tile_info.number_max;
        move_with_counter.number_available = move_tile_info.number_available;

        move_with_counter.GetMoveTile().GetComponent<MoveTileClick>().parent_panel = this.gameObject;
        move_with_counter.GetMoveTile().GetComponent<MoveTileClick>().coord_in_panel = y;
    }

    public void CreateMovesPanel(MoveInfo[] moves_info)
    {
        ClearMovesPanel();
        int moves_types_number = moves_info.Length;
        panel = new GameObject[moves_types_number + 1];

        for (int y = 0; y < moves_types_number; y++)
        {
            CreateMoveTile(y, moves_info[y], default_move_tile_type);
        }
        CreateMoveTile(moves_types_number, new MoveInfo(MoveTile.Direction.DELETE, 0, 0), MoveTile.Type.DELETE);
    }

    public void ClearMovesPanel()
    {
        foreach (GameObject panel_elem in panel)
        {
            Destroy(panel_elem);
        }
    }

    public void Click(int y)
    {
        MoveWithCounter move_tile_clicked = panel[y].GetComponent<MoveWithCounter>();

        if (move_tile_clicked.number_available <= 0 && move_tile_clicked.move_direction != MoveTile.Direction.DELETE)
        {
            return;
        }

        if (active_in_panel == NO_ACTIVE)
        {
            move_tile_clicked.move_type = MoveTile.Type.ACTIVE;
            active_in_panel = y;
            return;
        }

        MoveWithCounter move_tile_active = panel[active_in_panel].GetComponent<MoveWithCounter>();

        if (move_tile_active.move_direction == MoveTile.Direction.DELETE)
        {
            move_tile_active.move_type = MoveTile.Type.DELETE;
        }
        else
        {
            move_tile_active.move_type = default_move_tile_type;
        }

        if (y == active_in_panel)
        {
            active_in_panel = NO_ACTIVE;
        }
        else
        {
            move_tile_clicked.move_type = MoveTile.Type.ACTIVE;
            active_in_panel = y;
        }
    }

    public MoveTile.Direction TakeActiveMoveTile()
    {
        if (active_in_panel != NO_ACTIVE)
        {
            MoveWithCounter move_with_counter = panel[active_in_panel].GetComponent<MoveWithCounter>();
            MoveTile.Direction move_direction = move_with_counter.move_direction;

            if (move_direction != MoveTile.Direction.DELETE)
            {
                move_with_counter.number_available--;
                if (move_with_counter.number_available == 0)
                {
                    move_with_counter.move_type = default_move_tile_type;
                    active_in_panel = NO_ACTIVE;
                }
            }

            return move_direction;
        }
        return MoveTile.Direction.NO_DIRECTION;
    }

    public void ReturnMoveTile(MoveTile.Direction returned_direction)
    {
        foreach (GameObject panel_elem in panel)
        {
            MoveWithCounter move_with_counter = panel_elem.GetComponent<MoveWithCounter>();
            if (move_with_counter.move_direction == returned_direction)
            {
                move_with_counter.number_available++;
                return;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
