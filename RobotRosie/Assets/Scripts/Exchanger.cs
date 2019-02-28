using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exchanger : MonoBehaviour
{
    const float EXCHANGER_TILES_SHIFT_FACTOR = 2F;

    // Players' moves panels from which we will receive move tiles.
    public MovesPanel moves_panel_player_1, moves_panel_player_2;

    public GameObject init_exchanger_tile_object;
    ExchangerTile[] panel;

    // Used for initialisation of exchanger tiles for two players.
    ExchangerTile.Type[] exchanger_tiles = { ExchangerTile.Type.PLAYER1, ExchangerTile.Type.PLAYER2 };


    // Click from the ExchangerTile at the index 'x'.
    public void ClickByIndex(int x)
    {
        ExchangerTile exchanger_tile = panel[x];
        MovesPanel moves_panel;

        // Find out which player interacts with the exchanger.
        switch (exchanger_tile.type)
        {
            case ExchangerTile.Type.PLAYER1:
                moves_panel = moves_panel_player_1;
                break;
            case ExchangerTile.Type.PLAYER2:
                moves_panel = moves_panel_player_2;
                break;
            default:
                return;
        }

        // Find out which tile we try to put in the exchanger.
        Move.Direction active_direction = moves_panel.TakeActiveMoveTile();

        // If we don't have any tile selected, click does not work.
        if (active_direction == Move.Direction.NO_DIRECTION) return;

        // If there was tile in the exchanger, return it back to the moves panel.
        Move.Direction returned_direction = exchanger_tile.GetDirection();
        if (returned_direction != Move.Direction.NO_DIRECTION)
        {
            moves_panel.ReturnMoveTile(returned_direction);
        }

        // Put new tile into the exchanger if it was not DELETE tile.
        if (active_direction == Move.Direction.DELETE)
        {
            exchanger_tile.SetDirection(Move.Direction.NO_DIRECTION);
        }
        else 
        {
            exchanger_tile.SetDirection(active_direction);
        }

        // Try to exchange tiles as there was interaction with the exchanger
        // and probably we have two tiles ready for the exchange.
        ExchangeIfPossible();
    }

    // Try to exchange move tiles in the exchanger.
    void ExchangeIfPossible()
    {
        Move.Direction direction_player_1 = Move.Direction.NO_DIRECTION;
        Move.Direction direction_player_2 = Move.Direction.NO_DIRECTION;
        // Find move tils in the exchanger given by every player.
        foreach (ExchangerTile exchanger_tile in panel)
        { 
            switch (exchanger_tile.type)
            {
                case ExchangerTile.Type.PLAYER1:
                    direction_player_1 = exchanger_tile.GetDirection();
                    break;
                case ExchangerTile.Type.PLAYER2:
                    direction_player_2 = exchanger_tile.GetDirection();
                    break;
            }
        }

        // If both players have tiles in the exchanger, make an exchange.
        if (direction_player_1 != Move.Direction.NO_DIRECTION 
                && direction_player_2 != Move.Direction.NO_DIRECTION)
        {
            moves_panel_player_1.ExchangeMoveTile(direction_player_2, direction_player_1);
            moves_panel_player_2.ExchangeMoveTile(direction_player_1, direction_player_2);
            foreach (ExchangerTile exchanger_tile in panel)
            {
                exchanger_tile.SetDirection(Move.Direction.NO_DIRECTION);
            }
        }

    }


    // Create operations.
    void CreateExchangerTile(int x, ExchangerTile.Type exchanger_tile_type)
    {
        Vector3 left_top_coords = transform.position;
        Vector3 coords = new Vector3(left_top_coords.x + x * EXCHANGER_TILES_SHIFT_FACTOR, left_top_coords.y, left_top_coords.z);
        panel[x] = Instantiate(init_exchanger_tile_object, coords, new Quaternion()).GetComponent<ExchangerTile>();

        ExchangerTile exchanger_tile = panel[x];
        exchanger_tile.type = exchanger_tile_type;

        exchanger_tile.GetComponent<ExchangerTileClick>().parent = this.gameObject;
        exchanger_tile.GetComponent<ExchangerTileClick>().parent_index = x;
    }

    void CreateExchanger()
    {
        int exchanger_tiles_number = exchanger_tiles.Length;
        panel = new ExchangerTile[exchanger_tiles_number];

        for (int x = 0; x < exchanger_tiles_number; x++)
        {
            CreateExchangerTile(x, exchanger_tiles[x]);
        }
    }

    void Start()
    {
        CreateExchanger();
    }
}
