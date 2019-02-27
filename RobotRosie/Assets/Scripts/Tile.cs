using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type { EMPTY, PLAYER1, PLAYER2, START, END }

    public Sprite[] imgs;

    public Type img_type = 0;

    void ChangeImg()
    {
        if (imgs.Length > (int)img_type)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[(int)img_type];
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
