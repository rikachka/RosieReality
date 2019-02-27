using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public enum Direction { NO_DIRECTION, FORWARD, LEFT, RIGHT, DELETE }
    public enum Type { ACTIVE, PLAYER1, PLAYER2, DELETE }

    public Sprite[] imgs_types;
    public Sprite[] imgs_directions;

    public Type type = 0;
    public Direction direction = 0;

    GameObject back;

    void ChangeImg()
    {
        if (imgs_types.Length > (int)type)
        {
            back.GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];
        }
        if (imgs_directions.Length > (int)direction)
        {
            GetComponent<SpriteRenderer>().sprite = imgs_directions[(int)direction];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        back = transform.Find("Back").gameObject;
        //Debug.Log("Start in MoveTile");
        ChangeImg();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImg();
    }
}
