using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangerTile : MonoBehaviour
{
    public enum Type { PLAYER1, PLAYER2 }

    public Sprite[] imgs_types;

    public Type type;
    public Move.Direction direction = Move.Direction.NO_DIRECTION;

    public GameObject move;

    void ChangeImg()
    {
        move.GetComponent<Move>().direction = direction;
        GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];
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
