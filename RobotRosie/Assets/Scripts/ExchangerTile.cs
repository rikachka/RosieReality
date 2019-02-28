using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangerTile : MonoBehaviour
{
    public enum Type { PLAYER1, PLAYER2 }

    public Sprite[] imgs_types;
    public Sprite[] imgs_directions;

    public Type type;
    public MoveTile.Direction direction = MoveTile.Direction.NO_DIRECTION;

    GameObject back;

    void ChangeImg()
    {
        if (imgs_types.Length > (int)type)
        {
            back.GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];
        }

        if (direction == MoveTile.Direction.NO_DIRECTION)
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
        back = transform.Find("Back").gameObject;
        ChangeImg();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImg();
    }
}
