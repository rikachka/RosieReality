using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject init_tile;

    public GameObject moves_panel_player_1, moves_panel_player_2;

    GameObject[,] field;

    int size = 5;
    float step_factor = 1.1F;

    public void CreateTestDirections()
    {
        List<MoveTile.Direction>[,] directions =
        {
            {
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.RIGHT, MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ })
            },
            {
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ })
            },
            {
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.RIGHT, MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.LEFT, MoveTile.Direction.FORWARD })
            },
            {
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD })
            },
            {
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.FORWARD }),
                new List<MoveTile.Direction>(new MoveTile.Direction[]{ MoveTile.Direction.LEFT, MoveTile.Direction.FORWARD })
            }
        };

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                foreach (MoveTile.Direction direction in directions[y, x])
                {
                    field[y, x].GetComponent<FieldTile>().AddDirection(direction);
                }
            }
        }
    }

    public void CreateTilesTypes(FieldTile.Type[,] tiles_types)
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                FieldTile tile = field[y, x].GetComponent<FieldTile>();
                tile.type = tiles_types[y, x];
                tile.ClearDirections();
                if (tile.type == FieldTile.Type.START)
                {
                    tile.AddDirection(MoveTile.Direction.FORWARD);
                }
                tile.robot.GetComponent<Robot>().type = Robot.Type.EMPTY;
            }
        }
    }

    public void ClearRobotRoute()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                FieldTile tile = field[y, x].GetComponent<FieldTile>();
                tile.robot.GetComponent<Robot>().type = Robot.Type.EMPTY;
            }
        }
    }

    public void CreateField()
    {
        Vector3 left_top_coords = transform.position;
        field = new GameObject[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector3 coords = new Vector3(left_top_coords.x + x * step_factor, left_top_coords.y - y * step_factor, left_top_coords.z);
                field[y, x] = Instantiate(init_tile, coords, new Quaternion());

                field[y, x].GetComponent<FieldTileClick>().parent = this.gameObject;
                field[y, x].GetComponent<FieldTileClick>().parent_point = new Point(x, y);
            }
        }
    }

    public void Click(Point point)
    {
        int x = point.x;
        int y = point.y;
        FieldTile tile = field[y, x].GetComponent<FieldTile>();
        MovesPanel moves_panel;

        switch (tile.type)
        {
            case FieldTile.Type.PLAYER1:
                moves_panel = moves_panel_player_1.GetComponent<MovesPanel>();
                break;
            case FieldTile.Type.PLAYER2:
                moves_panel = moves_panel_player_2.GetComponent<MovesPanel>();
                break;
            default:
                return;
        }

        MoveTile.Direction active_direction = moves_panel.TakeActiveMoveTile();
        if (active_direction == MoveTile.Direction.DELETE)
        {
            MoveTile.Direction returned_direction = field[y, x].GetComponent<FieldTile>().DeleteDirection();
            if (returned_direction != MoveTile.Direction.NO_DIRECTION)
            {
                moves_panel.ReturnMoveTile(returned_direction);
            }

        }
        else if (active_direction != MoveTile.Direction.NO_DIRECTION)
        {
            field[y, x].GetComponent<FieldTile>().AddDirection(active_direction);
        }
    }

    Point FindStartTile()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (field[y, x].GetComponent<FieldTile>().type == FieldTile.Type.START)
                {
                    return new Point(x, y);
                }
            }
        }
        throw new System.Exception("No start tile");
    }

    public bool CheckWinningCondition()
    {
        Point point = FindStartTile();
        Robot robot = field[point.y, point.x].GetComponent<FieldTile>().robot.GetComponent<Robot>();

        bool[,] visited = new bool[size, size];
        while (!visited[point.y, point.x])
        {
            visited[point.y, point.x] = true;

            FieldTile tile = field[point.y, point.x].GetComponent<FieldTile>();
            if (tile.type == FieldTile.Type.EMPTY) return false;

            robot = tile.MoveRobotThrough(robot.direction);
            if (robot.type == Robot.Type.STOP)
            {
                if (tile.type == FieldTile.Type.END
                        && moves_panel_player_1.GetComponent<MovesPanel>().NumberAvailableMoves() == 0
                        && moves_panel_player_2.GetComponent<MovesPanel>().NumberAvailableMoves() == 0) 
                { 
                    return true; 
                }
                return false;
            }
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

            if (point.x < 0 || point.x >= size || point.y < 0 || point.y >= size) return false;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //CreateField();
        //CreateTestDirections();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
