using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Point used to identify element in the game field.
public struct Point
{
    public int x, y;

    public Point(int x_, int y_)
    {
        x = x_;
        y = y_;
    }
}

public class Field : MonoBehaviour
{
    const int FIELD_SIZE = 5;
    const float FIELD_TILES_SHIFT_FACTOR = 1.1F;

    // Players' moves panels from which we will receive move tiles.
    public MovesPanel moves_panel_player_1, moves_panel_player_2;

    public GameObject init_field_tile_object;
    FieldTile[,] field;


    // Click from the ExchangerTile at the 'point' coordinates.
    public void ClickByPoint(Point point)
    {
        FieldTile field_tile = field[point.y, point.x];

        // Find out which player interacts with the field.
        MovesPanel moves_panel;
        switch (field_tile.type)
        {
            case FieldTile.Type.PLAYER1:
                moves_panel = moves_panel_player_1;
                break;
            case FieldTile.Type.PLAYER2:
                moves_panel = moves_panel_player_2;
                break;
            default:
                return;
        }

        // Find out which tile we try to put in the exchanger.
        Move.Direction new_direction = moves_panel.TakeActiveMoveTile();
        switch (new_direction)
        {
            case Move.Direction.NO_DIRECTION:
                // If we don't have any tile selected, click does not work.
                return;
            case Move.Direction.DELETE:
                // Return tile back to the panel if any.
                Move.Direction returned_direction = field_tile.DeleteDirection();
                if (returned_direction != Move.Direction.NO_DIRECTION)
                {
                    moves_panel.ReturnMoveTile(returned_direction);
                }
                break;
            default:
                // Add move tile to the field tile.
                field_tile.AddDirection(new_direction);
                break;
        }
    }



    // Find position of the start tile to start the robot.
    Point FindStartTile()
    {
        for (int y = 0; y < FIELD_SIZE; y++)
        {
            for (int x = 0; x < FIELD_SIZE; x++)
            {
                if (field[y, x].type == FieldTile.Type.START)
                {
                    return new Point(x, y);
                }
            }
        }
        throw new System.Exception("No start tile");
    }

    // Check whether all win conditions are fulfilled:
    // - Robot stopped in the end tile.
    // - There are no available tiles in the players' moves panels.
    public bool CheckWinningCondition()
    {
        Point point = FindStartTile();
        Robot robot = field[point.y, point.x].robot.GetComponent<Robot>();

        // We do not allow to go through the same tile twice thus 
        // we mark tiles which have been visited.
        bool[,] visited = new bool[FIELD_SIZE, FIELD_SIZE];
        while (!visited[point.y, point.x])
        {
            visited[point.y, point.x] = true;

            FieldTile field_tile = field[point.y, point.x];

            // If we came to an obstacle, win conditions are not met.
            if (field_tile.type == FieldTile.Type.EMPTY) return false;

            // Find robot position after executing movements on this tile.
            robot = field_tile.MoveRobotThrough(robot.direction);
            if (robot.type == Robot.Type.STOP)
            {
                // If destinations is END tile and both players do not have 
                // available tiles in the moves panel, we win.
                if (field_tile.type == FieldTile.Type.END
                        && moves_panel_player_1.NumberAvailableMoves() == 0
                        && moves_panel_player_2.NumberAvailableMoves() == 0) 
                { 
                    return true; 
                }
                // Otherwise we stopped in a wrong place or did not use all tiles.
                return false;
            }
            // Find the next tile coordinates for the robot.
            if (robot.type == Robot.Type.MOVE)
            {
                switch (robot.direction)
                {
                    case Robot.Direction.RIGHT:
                        point.x += 1;
                        break;
                    case Robot.Direction.LEFT:
                        point.x -= 1;
                        break;
                    case Robot.Direction.UP:
                        point.y -= 1;
                        break;
                    case Robot.Direction.DOWN:
                        point.y += 1;
                        break;
                }
            }

            // If we try to leave the field, win conditions are not met.
            if (point.x < 0 || point.x >= FIELD_SIZE || point.y < 0 || point.y >= FIELD_SIZE) return false;
        }
        return false;
    }

    // Clear robot footprints left on the field.
    public void ClearRobotRoute()
    {
        for (int y = 0; y < FIELD_SIZE; y++)
        {
            for (int x = 0; x < FIELD_SIZE; x++)
            {
                FieldTile tile = field[y, x];
                tile.robot.GetComponent<Robot>().type = Robot.Type.EMPTY;
            }
        }
    }




    // Create / Update operations.
    public void CreateTilesTypes(FieldTile.Type[,] tiles_types)
    {
        for (int y = 0; y < FIELD_SIZE; y++)
        {
            for (int x = 0; x < FIELD_SIZE; x++)
            {
                FieldTile tile = field[y, x];
                tile.type = tiles_types[y, x];
                tile.ClearDirections();
                // Start tile always has one FORWARD tile.
                if (tile.type == FieldTile.Type.START)
                {
                    tile.AddDirection(Move.Direction.FORWARD);
                }
                tile.robot.GetComponent<Robot>().type = Robot.Type.EMPTY;
            }
        }
    }

    public void CreateField()
    {
        Vector3 left_top_coords = transform.position;
        field = new FieldTile[FIELD_SIZE, FIELD_SIZE];

        for (int y = 0; y < FIELD_SIZE; y++)
        {
            for (int x = 0; x < FIELD_SIZE; x++)
            {
                Vector3 coords = new Vector3(left_top_coords.x + x * FIELD_TILES_SHIFT_FACTOR, left_top_coords.y - y * FIELD_TILES_SHIFT_FACTOR, left_top_coords.z);
                field[y, x] = Instantiate(init_field_tile_object, coords, new Quaternion()).GetComponent<FieldTile>();

                field[y, x].GetComponent<FieldTileClick>().parent = this.gameObject;
                field[y, x].GetComponent<FieldTileClick>().parent_point = new Point(x, y);
            }
        }
    }

    public void UpdateDirections(List<Move.Direction>[,] directions)
    {
        for (int y = 0; y < FIELD_SIZE; y++)
        {
            for (int x = 0; x < FIELD_SIZE; x++)
            {
                FieldTile tile = field[y, x];
                tile.ClearDirections();
                if (tile.type == FieldTile.Type.START)
                {
                    tile.AddDirection(Move.Direction.FORWARD);
                }
                foreach (Move.Direction direction in directions[y, x])
                {
                    field[y, x].AddDirection(direction);
                }
            }
        }
    }

    void Start()
    {
        //CreateField();
    }
}
