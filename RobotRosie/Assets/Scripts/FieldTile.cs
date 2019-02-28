using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    public enum Type { EMPTY, PLAYER1, PLAYER2, START, END }

    public Sprite[] imgs_types;

    public Type type;

    public GameObject robot;

    // Object showing the last movement attached to the tile.
    public GameObject last_direction_tile;
    // Container of movements attached to the tile.
    List<Move.Direction> directions = new List<Move.Direction>();

    public void AddDirection(Move.Direction direction)
    {
        directions.Add(direction);
    }

    public Move.Direction DeleteDirection()
    {
        if (directions.Count > 0)
        {
            Move.Direction direction = directions[directions.Count - 1];
            directions.RemoveAt(directions.Count - 1);
            return direction;
        }
        else
        {
            return Move.Direction.NO_DIRECTION;
        }
    }

    public void ClearDirections()
    {
        directions = new List<Move.Direction>();
    }

    public Robot MoveRobotThrough(Robot.Direction prev_direction)
    {
        int direction_change = 0;
        foreach (Move.Direction direction in directions)
        {
            switch (direction)
            {
                case Move.Direction.LEFT:
                    direction_change++;
                    break;
                case Move.Direction.RIGHT:
                    direction_change--;
                    break;
                case Move.Direction.FORWARD:
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
            Move.Direction last_direction = directions[directions.Count - 1];
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
