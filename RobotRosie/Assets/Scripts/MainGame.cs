using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public enum State { GAME, CHECK, WIN }

    public MovesPanel moves_panel_player_1, moves_panel_player_2; 
    public Field field;

    State state = State.GAME;

    private void OnGUI()
    {
        float screen_centre_x = Screen.width / 2;
        float screen_centre_y = Screen.height / 2;

        Rect location_button = new Rect(new Vector2(screen_centre_x - 100, 10), new Vector2(200, 30));
        Rect location_box = new Rect(new Vector2(10, 10), new Vector2(Screen.width - 20, Screen.height - 20));
        Rect location_label = new Rect(new Vector2(screen_centre_x - 110, screen_centre_y - 30), new Vector2(220, 30));

        switch (state)
        {
            case State.WIN:
                // If win conditions are met, show congratulations and propose to start new game.
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
                // Default game state. It is possible to place, remove or exchange move tiles.
                if (GUI.Button(location_button, "Play"))
                {
                    if (field.CheckWinningCondition())
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
                // If robot does not reach the end tile after the player pressed the “play” button, 
                // show robot's route and the button to continue the game (place or remove move tiles 
                // before “playing” the program once more).
                GUI.Box(location_box, "");
                if (GUI.Button(location_button, "Continue"))
                {
                    field.ClearRobotRoute();
                    state = State.GAME;
                }
                break;
        }
    }



    // Hardcoded new game.
    void CreateNewGame()
    {
        MovesPanel.MoveWithCounterInfo[] moves_info_1 =
        {
            new MovesPanel.MoveWithCounterInfo(Move.Direction.FORWARD, 8, 8),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.LEFT, 0, 0),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.RIGHT, 1, 1)
        };
        moves_panel_player_1.UpdateMovesPanel(moves_info_1);

        MovesPanel.MoveWithCounterInfo[] moves_info_2 =
        {
            new MovesPanel.MoveWithCounterInfo(Move.Direction.FORWARD, 6, 6),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.LEFT, 2, 2),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.RIGHT, 1, 1)
        };
        moves_panel_player_2.UpdateMovesPanel(moves_info_2);


        FieldTile.Type[,] tiles_types =
        {
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.END },
            { FieldTile.Type.PLAYER1, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2 },
            { FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.EMPTY, FieldTile.Type.START, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1 },
        };
        field.CreateTilesTypes(tiles_types);
    }

    // Hardcoded solved game to test win conditions.
    void CreateSolvedGame()
    {
        MovesPanel.MoveWithCounterInfo[] moves_info_1 =
        {
            new MovesPanel.MoveWithCounterInfo(Move.Direction.FORWARD, 6, 0),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.LEFT, 1, 0),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.RIGHT, 2, 0)
        };
        moves_panel_player_1.UpdateMovesPanel(moves_info_1);

        MovesPanel.MoveWithCounterInfo[] moves_info_2 =
        {
            new MovesPanel.MoveWithCounterInfo(Move.Direction.FORWARD, 8, 0),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.LEFT, 1, 0),
            new MovesPanel.MoveWithCounterInfo(Move.Direction.RIGHT, 0, 0)
        };
        moves_panel_player_2.UpdateMovesPanel(moves_info_2);


        FieldTile.Type[,] tiles_types =
        {
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.END },
            { FieldTile.Type.PLAYER1, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1, FieldTile.Type.PLAYER2 },
            { FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.EMPTY, FieldTile.Type.PLAYER1 },
            { FieldTile.Type.EMPTY, FieldTile.Type.START, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER2, FieldTile.Type.PLAYER1 },
        };
        field.CreateTilesTypes(tiles_types);


        List<Move.Direction>[,] directions =
        {
            {
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.RIGHT, Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ })
            },
            {
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ })
            },
            {
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.RIGHT, Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.LEFT, Move.Direction.FORWARD })
            },
            {
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD })
            },
            {
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.FORWARD }),
                new List<Move.Direction>(new Move.Direction[]{ Move.Direction.LEFT, Move.Direction.FORWARD })
            }
        };
        field.UpdateDirections(directions);
    }



    void Start()
    {
        field.CreateField();
        moves_panel_player_1.CreateMovesPanel();
        moves_panel_player_2.CreateMovesPanel();

        //CreateNewGame();
        CreateSolvedGame();
    }
}
