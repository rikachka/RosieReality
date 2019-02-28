using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exchanger : MonoBehaviour
{
    GameObject[] panel;
    public GameObject init_exchanger_tile;
    public GameObject moves_panel_player_1, moves_panel_player_2;

    ExchangerTile.Type[] exchanger_tiles = { ExchangerTile.Type.PLAYER1, ExchangerTile.Type.PLAYER2 };

    float step_factor = 2F;

    void CreateExchangerTile(int x, ExchangerTile.Type exchanger_tile_type)
    {
        Vector3 left_top_coords = transform.position;
        Vector3 coords = new Vector3(left_top_coords.x + x * step_factor, left_top_coords.y, left_top_coords.z);
        panel[x] = Instantiate(init_exchanger_tile, coords, new Quaternion());

        ExchangerTile exchanger_tile = panel[x].GetComponent<ExchangerTile>();
        exchanger_tile.type = exchanger_tile_type;

        exchanger_tile.GetComponent<ExchangerTileClick>().parent = this.gameObject;
        exchanger_tile.GetComponent<ExchangerTileClick>().parent_index = x;
    }

    public void CreateExchanger()
    {
        int exchanger_tiles_number = exchanger_tiles.Length;
        panel = new GameObject[exchanger_tiles_number];

        for (int x = 0; x < exchanger_tiles_number; x++)
        {
            CreateExchangerTile(x, exchanger_tiles[x]);
        }
    }

    public void Click(int x)
    {
        ExchangerTile exchanger_tile = panel[x].GetComponent<ExchangerTile>();
        MovesPanel moves_panel;

        switch (exchanger_tile.type)
        {
            case ExchangerTile.Type.PLAYER1:
                moves_panel = moves_panel_player_1.GetComponent<MovesPanel>();
                break;
            case ExchangerTile.Type.PLAYER2:
                moves_panel = moves_panel_player_2.GetComponent<MovesPanel>();
                break;
            default:
                return;
        }

        MoveTile.Direction active_direction = moves_panel.TakeActiveMoveTile();

        if (active_direction == MoveTile.Direction.NO_DIRECTION) return;

        MoveTile.Direction returned_direction = exchanger_tile.direction;

        if (returned_direction != MoveTile.Direction.NO_DIRECTION)
        {
            moves_panel.ReturnMoveTile(returned_direction);
        }

        if (active_direction == MoveTile.Direction.DELETE)
        {
            exchanger_tile.direction = MoveTile.Direction.NO_DIRECTION;
        }
        else 
        {
            exchanger_tile.direction = active_direction;
        }

        ExchangeIfPossible();
    }

    void ExchangeIfPossible()
    {
        //foreach (GameObject panel_elem in panel)
        //{
        //    ExchangerTile exchanger_tile = panel_elem.GetComponent<ExchangerTile>();
        //    if (exchanger_tile.direction == MoveTile.Direction.NO_DIRECTION) return;
        //}

        //foreach (GameObject panel_elem in panel)
        //{
        //    ExchangerTile exchanger_tile = panel_elem.GetComponent<ExchangerTile>();
        //    MovesPanel moves_panel;

        //    switch (exchanger_tile.type)
        //    {
        //        case ExchangerTile.Type.PLAYER1:
        //            moves_panel = moves_panel_player_2.GetComponent<MovesPanel>();
        //            break;
        //        case ExchangerTile.Type.PLAYER2:
        //            moves_panel = moves_panel_player_1.GetComponent<MovesPanel>();
        //            break;
        //        default:
        //            return;
        //    }
        //    moves_panel.ReturnMoveTile(exchanger_tile.direction);
        //    exchanger_tile.direction = MoveTile.Direction.NO_DIRECTION;
        //}


        //foreach (GameObject panel_elem in panel)
        //{
        //    ExchangerTile exchanger_tile = panel_elem.GetComponent<ExchangerTile>();
        //    if (exchanger_tile.direction == MoveTile.Direction.NO_DIRECTION) return;
        //}


        MoveTile.Direction direction_player_1 = MoveTile.Direction.NO_DIRECTION;
        MoveTile.Direction direction_player_2 = MoveTile.Direction.NO_DIRECTION;
        foreach (GameObject panel_elem in panel)
        {
            ExchangerTile exchanger_tile = panel_elem.GetComponent<ExchangerTile>(); 
            switch (exchanger_tile.type)
            {
                case ExchangerTile.Type.PLAYER1:
                    direction_player_1 = exchanger_tile.direction;
                    break;
                case ExchangerTile.Type.PLAYER2:
                    direction_player_2 = exchanger_tile.direction;
                    break;
            }
        }

        if (direction_player_1 != MoveTile.Direction.NO_DIRECTION 
                && direction_player_2 != MoveTile.Direction.NO_DIRECTION)
        {
            moves_panel_player_1.GetComponent<MovesPanel>().ExchangeMoveTile(direction_player_2, direction_player_1);
            moves_panel_player_2.GetComponent<MovesPanel>().ExchangeMoveTile(direction_player_1, direction_player_2);
            foreach (GameObject panel_elem in panel)
            {
                panel_elem.GetComponent<ExchangerTile>().direction = MoveTile.Direction.NO_DIRECTION;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        CreateExchanger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
