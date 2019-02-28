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

        Rect location_button = new Rect(new Vector2(screen_centre_x - 100, 10), new Vector2(200, 30));
        Rect location_box = new Rect(new Vector2(10, 10), new Vector2(Screen.width - 20, Screen.height - 20));
        Rect location_label = new Rect(new Vector2(screen_centre_x - 110, screen_centre_y - 30), new Vector2(220, 30));

        switch (state) {
            case State.WIN:
                GUI.Box(location_box, "");

                GUIStyle gui_style = new GUIStyle();
                gui_style.fontSize = 30;
                gui_style.normal.textColor = Color.white;
                GUI.Label(location_label, "Congratulations!", gui_style);

                if (GUI.Button(location_button, "New game"))
                {
                    CreateNewGame();
                    state = State.GAME;
                }
                break;
            case State.GAME:
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
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 8, 8),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 0, 0),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 1, 1)
        };
        moves_panel_player_1.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_1);

        MovesPanel.MoveInfo[] moves_info_2 = 
        {
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 6, 6),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 2, 2),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 1, 1)
        };
        moves_panel_player_2.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_2);


        FieldTile.Type[,] tiles_types =
        {
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.END },
            { FieldTile.Type.PLAYER1, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2 },
            { FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.EMPTY, FieldTile.Type.START, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1 },
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


        FieldTile.Type[,] tiles_types =
        {
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.END },
            { FieldTile.Type.PLAYER1, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2 },
            { FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.EMPTY, FieldTile.Type.START, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1 },
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
