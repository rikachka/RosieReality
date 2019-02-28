using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangerTile : MonoBehaviour
{
    public enum Type { PLAYER1, PLAYER2 }

    public Type type;

    public Sprite[] imgs_types;

    public Move move;

    public Move.Direction GetDirection()
    {
        return move.direction;
    }

    public void SetDirection(Move.Direction direction)
    {
        move.direction = direction;
    }

    void ChangeImg()
    {
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
