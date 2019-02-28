using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    public enum Type { EMPTY, PLAYER1, PLAYER2, START, END }

    public Type type;

    public Sprite[] imgs_types;

    // Robot step through the tile.
    public Robot robot;

    // The last movement attached to the tile.
    public Move last_move;

    // Container of movements attached to the tile.
    // Movements will be executed in the natural order.
    List<Move.Direction> directions = new List<Move.Direction>();


    // Operations with the movements attached to the file.
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


    // Given the direction of the robot at the previous tile, calculate
    // and return its position after executing the movements on this tile.
    public Robot MoveRobotThrough(Robot.Direction prev_direction)
    {
        // Spin in the array { RIGHT, UP, LEFT, DOWN } due to the direction of the move.
        int direction_change = 0;
        foreach (Move.Direction direction in directions)
        {
            switch (direction)
            {
                case Move.Direction.LEFT:
                    // 
                    direction_change++;
                    break;
                case Move.Direction.RIGHT:
                    direction_change--;
                    break;
                case Move.Direction.FORWARD:
                    // If we found forward direction, we calculate the direction
                    // of the robot's movement from this tile and leave the tile.
                    robot.FindDirection(prev_direction, direction_change);
                    robot.type = Robot.Type.MOVE;
                    return robot;
                default:
                    robot.type = Robot.Type.STOP;
                    break;
            }
        }
        // If we did not leave the tile, that is robot's final destination.
        robot.type = Robot.Type.STOP;
        return robot;
    }


    void ChangeImg()
    {
        // Always show robot direction on the start tile.
        if (type == Type.START) robot.type = Robot.Type.MOVE;

        // Color tile due to it's type: start, end, obstacle, belonging to a player.
        GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];

        // Show the last movement attached to the tile if any.
        if (directions.Count <= 0)
        {
            last_move.direction = Move.Direction.NO_DIRECTION;
        }
        else
        {
            last_move.direction = directions[directions.Count - 1];
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
