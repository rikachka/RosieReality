using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public enum State { GAME, CHECK, WIN }

    public GameObject moves_panel_player_1, moves_panel_player_2, field;

    State state = State.GAME;

    private void OnGUI()
    {
        float screen_centre_x = Screen.width / 2;
        float screen_centre_y = Screen.height / 2;

        Rect location_button;

        switch (state) {
            case State.WIN:
                location_button = new Rect(new Vector2(10, 10), new Vector2(Screen.width - 20, Screen.height - 20));
                GUI.Box(location_button, "");

                location_button = new Rect(new Vector2(screen_centre_x - 100, screen_centre_y - 15), new Vector2(200, 30));
                GUIStyle gui_style = new GUIStyle();
                gui_style.fontSize = 30;
                gui_style.normal.textColor = Color.white;
                GUI.Label(location_button, "Congratulations!", gui_style);


                location_button = new Rect(new Vector2(screen_centre_x - 93, 10), new Vector2(200, 30));
                if (GUI.Button(location_button, "New game"))
                {
                    CreateNewGame();
                    state = State.GAME;
                }
                break;
            case State.GAME:
                location_button = new Rect(new Vector2(screen_centre_x - 93, 10), new Vector2(200, 30));
                if (GUI.Button(location_button, "Check"))
                {
                    if (field.GetComponent<Field>().CheckWinningCondition())
                    {
                        state = State.WIN;
                    }
                    else
                    {
                        state = State.CHECK;
                    }
                }
                break;
            case State.CHECK:
                location_button = new Rect(new Vector2(screen_centre_x - 93, 10), new Vector2(200, 30));
                if (GUI.Button(location_button, "Continue"))
                {
                    field.GetComponent<Field>().ClearRobotRoute();
                    state = State.GAME;
                }
                break;
        }
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
            { Tile.Type.PLAYER1, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.PLAYER1 },
            { Tile.Type.PLAYER1, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1, Tile.Type.PLAYER2 },
            { Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.EMPTY, Tile.Type.PLAYER1 },
            { Tile.Type.EMPTY, Tile.Type.START, Tile.Type.PLAYER2, Tile.Type.PLAYER2, Tile.Type.PLAYER1 },
        };
        field.GetComponent<Field>().CreateTilesTypes(tiles_types);
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
        field.GetComponent<Field>().CreateTilesTypes(tiles_types);

        field.GetComponent<Field>().CreateTestDirections();
    }

    // Start is called before the first frame update
    void Start()
    {
        field.GetComponent<Field>().CreateField();
        CreateNewGame();
        //CreateSolvedGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
