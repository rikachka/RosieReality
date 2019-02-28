using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public enum Type { EMPTY, MOVE, STOP }
    public enum Direction { RIGHT, UP, LEFT, DOWN }

    public Sprite[] imgs_types;
    public Sprite[] imgs_directions;

    public Type type = Type.EMPTY;
    public Direction direction;


    // Given the previous direction of the robot, calculate its next direction
    // after executing the turn representing by 'direction_change'.
    // Spin in the array { RIGHT, UP, LEFT, DOWN } 'direction_change' times
    // starting from the 'prev_direction'.
    public void FindDirection(Direction prev_direction, int direction_change)
    {
        int directions_number = System.Enum.GetNames(typeof(Type)).Length;
        int new_direction = (int)prev_direction + direction_change;
        direction = (Direction)(new_direction % directions_number);
    }


    void ChangeImg()
    {
        switch (type)
        {
            case Type.MOVE:
                // Robot moved through the tile, show the direction to the next tile.
                if (imgs_directions.Length > (int)direction)
                {
                    GetComponent<SpriteRenderer>().sprite = imgs_directions[(int)direction];
                }
                break;
            case Type.STOP:
                // Robot stopped on the tile.
                if (imgs_directions.Length > (int)direction)
                {
                    GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];
                }
                break;
            default:
                // Robot did not leave a footprint on the tile, thus do not show anything.
                GetComponent<SpriteRenderer>().sprite = null;
                break;
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
