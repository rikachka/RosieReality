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

    public void FindDirection(Direction prev_direction, int direction_change)
    {
        int directions_number = System.Enum.GetNames(typeof(Type)).Length;
        int new_direction = (int)prev_direction + direction_change;
        direction = (Direction)(new_direction % directions_number);
        type = Type.MOVE;
    }

    void ChangeImg()
    {
        switch (type)
        {
            case Type.MOVE:
                if (imgs_directions.Length > (int)direction)
                {
                    GetComponent<SpriteRenderer>().sprite = imgs_directions[(int)direction];
                }
                break;
            case Type.STOP:
                if (imgs_directions.Length > (int)direction)
                {
                    GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];
                }
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeImg();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImg();
    }
}
