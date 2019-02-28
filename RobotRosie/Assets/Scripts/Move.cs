using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public enum Direction { NO_DIRECTION, FORWARD, LEFT, RIGHT, DELETE }

    public Sprite[] imgs_directions;

    public Direction direction = Direction.NO_DIRECTION;

    void ChangeImg()
    {
        if (direction == Direction.NO_DIRECTION)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imgs_directions[(int)direction];
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
