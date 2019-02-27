using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameObject moves_panel_player_1, moves_panel_player_2, field;

    private void OnGUI()
    {
        float screen_centre_x = Screen.width / 2;
        float screen_centre_y = Screen.height / 2;

        //Camera cam = GetComponent<Camera>();
        Rect location_button;

        //location_button = new Rect(new Vector2(screen_centre_x - 150, 0), new Vector2(300, 200));
        //GUI.Box(location_button, "");

        //location_button = new Rect(new Vector2(screen_centre_x - 10, 10), new Vector2(200, 30));
        //GUI.Label(location_button, "Menu");

        location_button = new Rect(new Vector2(screen_centre_x - 110, 10), new Vector2(200, 30));
        GUI.Button(location_button, "Check");
    }

    void CreateNewGame()
    {
        MovesPanel.MoveInfo[] moves_info_1 = 
        {
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 6, 6),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 1, 1),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 2, 2)
        };
        moves_panel_player_1.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_1);

        MovesPanel.MoveInfo[] moves_info_2 = 
        {
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 8, 8),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 1, 1),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 0, 0)
        };
        moves_panel_player_2.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_2);


        Tile.Type[,] tiles_types =
        {
            { Tile.Type.PLAYER1, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.END },
            { Tile.Type.PLAYER1, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY },
            { Tile.Type.PLAYER1, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1, Tile.Type.PLAYER2 },
            { Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.PLAYER1 },
            { Tile.Type.EMPTY, Tile.Type.START, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1 },
        };
        field.GetComponent<Field>().CreateField(tiles_types);
    }

    void CreateSolvedGame()
    {
        MovesPanel.MoveInfo[] moves_info_1 =
        {
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 6, 0),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 1, 0),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 2, 0)
        };
        moves_panel_player_1.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_1);

        MovesPanel.MoveInfo[] moves_info_2 =
        {
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 8, 0),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 1, 0),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 0, 0)
        };
        moves_panel_player_2.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_2);


        Tile.Type[,] tiles_types =
        {
            { Tile.Type.PLAYER1, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.END },
            { Tile.Type.PLAYER1, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY },
            { Tile.Type.PLAYER1, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1, Tile.Type.PLAYER2 },
            { Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.PLAYER1 },
            { Tile.Type.EMPTY, Tile.Type.START, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1 },
        };
        field.GetComponent<Field>().CreateField(tiles_types);

        field.GetComponent<Field>().CreateTestDirections();
    }

    // Start is called before the first frame update
    void Start()
    {
        //CreateNewGame();
        CreateSolvedGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
