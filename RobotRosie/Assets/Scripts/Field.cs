using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public enum TileType { EMPTY, PLAYER1, PLAYER2, START, END }

    public GameObject tile;

    GameObject[,] field;
    TileType[,] tiles_types = 
    { 
        { TileType.PLAYER1, TileType.PLAYER2, TileType.PLAYER2, TileType.PLAYER2, TileType.END },
        { TileType.PLAYER1, TileType.EMPTY, TileType.EMPTY, TileType.EMPTY, TileType.EMPTY },
        { TileType.PLAYER1, TileType.PLAYER2, TileType.PLAYER2, TileType.PLAYER1, TileType.PLAYER2 },
        { TileType.EMPTY, TileType.EMPTY, TileType.EMPTY, TileType.EMPTY, TileType.PLAYER1 },
        { TileType.EMPTY, TileType.START, TileType.PLAYER2, TileType.PLAYER2, TileType.PLAYER1 },
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
                field[y, x] = Instantiate(tile);
                field[y, x].GetComponent<Tile>().img_type = (int)tiles_types[y, x];
                field[y, x].transform.position = new Vector3(left_top_coords.x + x * step_factor, left_top_coords.y - y * step_factor, left_top_coords.z);

                field[y, x].GetComponent<ClickTile>().parent = this.gameObject;
                field[y, x].GetComponent<ClickTile>().x = x;
                field[y, x].GetComponent<ClickTile>().y = y;
            }
        }
    }

    public void Click(int x, int y)
    {
        field[y, x].GetComponent<Tile>().img_type = 4;
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
