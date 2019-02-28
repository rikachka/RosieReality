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
        else if (imgs.Length > (int)img_type)
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
