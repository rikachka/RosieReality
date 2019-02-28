using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type { EMPTY, PLAYER1, PLAYER2, START, END }

    public Sprite[] imgs_types;
    public Sprite[] imgs_directions;

    public Type type = 0;

    public Robot.Type robot_type = Robot.Type.EMPTY;

    GameObject last_direction_tile; 
    //public Robot robot;
    public GameObject robot;

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
        return MoveTile.Direction.NO_DIRECTION;
    }

    public void ClearDirections()
    {
        directions = new List<MoveTile.Direction>();
    }

    public Robot MoveRobotThrough(Robot.Direction prev_direction)
    {
        int direction_change = 0;
        Robot robot_component = robot.GetComponent<Robot>();
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
                    robot_component.FindDirection(prev_direction, direction_change);
                    //ChangeImg();
                    return robot_component;
                default:
                    robot_component.type = Robot.Type.STOP;
                    break;
            }
        }
        robot_component.type = Robot.Type.STOP;
        //ChangeImg();
        return robot_component;
    }

    void ChangeImg()
    {
        //robot.GetComponent<Robot>().type = robot_type;
        //robot.type = robot_type;

        if (imgs_types.Length > (int)type)
        {
            GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];
        }

        if (directions.Count <= 0)
        {
            last_direction_tile.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            MoveTile.Direction last_direction = directions[directions.Count - 1];
            last_direction_tile.GetComponent<SpriteRenderer>().sprite = imgs_directions[(int)last_direction];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        last_direction_tile = transform.Find("LastDirection").gameObject;
        //robot = transform.Find("Robot").GetComponent<Robot>();
        //robot = transform.Find("Robot");

        robot.GetComponent<Robot>().type = robot_type;
        ChangeImg();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImg();
    }
}
