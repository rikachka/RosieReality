using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public Sprite[] imgs;

    public int img_type = 0;

    void ChangeImg()
    {
        if (imgs.Length > img_type)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[img_type];
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
