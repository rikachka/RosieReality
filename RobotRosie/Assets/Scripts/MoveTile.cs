using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public enum Type { ACTIVE, PLAYER1, PLAYER2, DELETE }

    public Sprite[] imgs_types;

    public Type type = 0;
    public Move.Direction direction = 0;

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
