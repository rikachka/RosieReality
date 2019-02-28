using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public enum Type { ACTIVE, PLAYER1, PLAYER2, DELETE }

    public Sprite[] imgs_types;

    public Move move;

    public Type type;

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
