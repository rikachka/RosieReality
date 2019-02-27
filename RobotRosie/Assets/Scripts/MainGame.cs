using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameObject moves_panel_player_1, moves_panel_player_2, field;

    private void OnGUI()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        MovesPanel.MoveInfo[] moves_info_1 = new MovesPanel.MoveInfo[]
        {
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 8, 8),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 0, 0),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 1, 1)
        };
        moves_panel_player_1.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_1);

        MovesPanel.MoveInfo[] moves_info_2 = new MovesPanel.MoveInfo[]
        {
            new MovesPanel.MoveInfo(MoveTile.Direction.FORWARD, 6, 6),
            new MovesPanel.MoveInfo(MoveTile.Direction.LEFT, 2, 2),
            new MovesPanel.MoveInfo(MoveTile.Direction.RIGHT, 1, 1)
        };
        moves_panel_player_2.GetComponent<MovesPanel>().CreateMovesPanel(moves_info_2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
