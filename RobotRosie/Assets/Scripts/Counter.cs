using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public enum Type { EMPTY, AVAILABLE, NO_COUNTER }

    public Sprite[] imgs;

    public Type img_type;

    void ChangeImg()
    {
        if (img_type == Type.NO_COUNTER)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else 
        {
            GetComponent<SpriteRenderer>().sprite = imgs[(int)img_type];
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
