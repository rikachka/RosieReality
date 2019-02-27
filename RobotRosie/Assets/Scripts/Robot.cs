using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public enum Direction { RIGHT, UP, LEFT, DOWN }

    public Sprite[] imgs_directions;

    public Direction direction;

    public bool is_shown = false;

    void ChangeImg()
    {
        if (!is_shown)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (imgs_directions.Length > (int)direction)
        {
            GetComponent<SpriteRenderer>().sprite = imgs_directions[(int)direction];
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
