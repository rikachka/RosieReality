using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public enum Type { NO_COUNTER, EMPTY, AVAILABLE }

    public Sprite[] imgs_types;

    public Type type;

    void ChangeImg()
    {
        if (type == Type.NO_COUNTER)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else 
        {
            GetComponent<SpriteRenderer>().sprite = imgs_types[(int)type];
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
