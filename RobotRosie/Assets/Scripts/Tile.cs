using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type { EMPTY, PLAYER1, PLAYER2, START, END }

    public Sprite[] imgs_types;
    public Sprite[] imgs_directions;

    public Type img_type = 0;

    public bool is_robot_shown = false;

    GameObject last_direction_tile; 
    Robot robot;

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

    public int MoveRobotThrough()
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
                    return direction_change;
                default:
                    return -1;
            }
        }
        return -1;
    }

    void ChangeImg()
    {
        robot.is_shown = is_robot_shown;

        if (imgs_types.Length > (int)img_type)
        {
            GetComponent<SpriteRenderer>().sprite = imgs_types[(int)img_type];
        }

        if (directions.Count <= 0)
        {
            last_direction_tile.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            //Debug.Log(directions.Count);
            MoveTile.Direction last_direction = directions[directions.Count - 1];
            last_direction_tile.GetComponent<SpriteRenderer>().sprite = imgs_directions[(int)last_direction];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        last_direction_tile = transform.Find("LastDirection").gameObject;
        robot = transform.Find("Robot").GetComponent<Robot>();

        ChangeImg();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImg();
    }
}
