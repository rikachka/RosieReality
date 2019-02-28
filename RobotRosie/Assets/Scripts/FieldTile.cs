using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    public enum Type { EMPTY, PLAYER1, PLAYER2, START, END }

    public Sprite[] imgs_types;
    public Sprite[] imgs_directions;

    public Type type = 0;

    public GameObject robot;

    // Object showing the last movement attached to the tile.
    public GameObject last_direction_tile;
    // Container of movements attached to the tile.
    List<MoveTile.Direction> directions = new List<MoveTile.Direction>();

    public void AddDirection(MoveTile.Direction direction)
    {
        directions.Add(direction);
    }

    public MoveTile.Direction DeleteDirection()
    {
        if (directions.Count > 0)
        {
            MoveTile.Direction direction = directions[directions.Count - 1];
            directions.RemoveAt(directions.Count - 1);
            return direction;
        }
        else
        {
            return MoveTile.Direction.NO_DIRECTION;
        }
    }

    public void ClearDirections()
    {
        directions = new List<MoveTile.Direction>();
    }

    public Robot MoveRobotThrough(Robot.Direction prev_direction)
    {
        int direction_change = 0;
        foreach (MoveTile.Direction direction in directions)
        {
            switch (direction)
            {
                case MoveTile.Direction.LEFT:
                    direction_change++;
                    break;
                case MoveTile.Direction.RIGHT:
                    direction_change--;
                    break;
                case MoveTile.Direction.FORWARD:
                    robot.GetComponent<Robot>().FindDirection(prev_direction, direction_change);
                    return robot.GetComponent<Robot>();
                default:
                    robot.GetComponent<Robot>().type = Robot.Type.STOP;
                    break;
            }
        }
        robot.GetComponent<Robot>().type = Robot.Type.STOP;
        return robot.GetComponent<Robot>();
    }

    void ChangeImg()
    {
        // Always show robot direction on the start tile.
        if (type == Type.START)
        {
            robot.GetComponent<Robot>().type = Robot.Type.MOVE;
        }

        // Color tile due to it's type: start, end, obstacle, belonging to a player.
        GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];

        // Show the last movement attached to the tile if any.
        if (directions.Count <= 0)
        {
            last_direction_tile.GetComponent<Move>().direction = Move.Direction.NO_DIRECTION;
        }
        else
        {
            MoveTile.Direction last_direction = directions[directions.Count - 1];
            last_direction_tile.GetComponent<Move>().direction = (Move.Direction)last_direction;
        }
    }

    void Start()
    {
        ChangeImg();
    }

    void Update()
    {
        ChangeImg();
    }
}
