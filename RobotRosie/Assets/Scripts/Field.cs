using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject init_tile;

    public GameObject moves_panel_player_1, moves_panel_player_2;

    GameObject[,] field;
    Tile.Type[,] tiles_types = 
    { 
        { Tile.Type.PLAYER1, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.END },
        { Tile.Type.PLAYER1, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY },
        { Tile.Type.PLAYER1, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1, Tile.Type.PLAYER2 },
        { Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.PLAYER1 },
        { Tile.Type.EMPTY, Tile.Type.START, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1 },
    };

    int size = 5;
    float step_factor = 1.1F;

    void CreateField()
    {
        Vector3 left_top_coords = transform.position;
        field = new GameObject[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector3 coords = new Vector3(left_top_coords.x + x * step_factor, left_top_coords.y - y * step_factor, left_top_coords.z);
                field[y, x] = Instantiate(init_tile, coords, new Quaternion());
                field[y, x].GetComponent<Tile>().img_type = tiles_types[y, x];

                field[y, x].GetComponent<TileClick>().parent = this.gameObject;
                field[y, x].GetComponent<TileClick>().x = x;
                field[y, x].GetComponent<TileClick>().y = y;
            }
        }
    }

    public void Click(int x, int y)
    {
        Tile tile = field[y, x].GetComponent<Tile>();
        MovesPanel moves_panel;

        switch (tile.img_type)
        {
            case Tile.Type.PLAYER1:
                moves_panel = moves_panel_player_1.GetComponent<MovesPanel>();
                break;
            case Tile.Type.PLAYER2:
                moves_panel = moves_panel_player_2.GetComponent<MovesPanel>();
                break;
            default:
                return;
        }

        MoveTile.Direction direction = moves_panel.TakeActiveMoveTile();
        if (direction != MoveTile.Direction.NO_DIRECTION)
        {
            field[y, x].GetComponent<Tile>().img_type = Tile.Type.END;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
