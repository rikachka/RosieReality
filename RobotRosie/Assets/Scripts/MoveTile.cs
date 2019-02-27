using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public Sprite[] imgs_types;
    public Sprite[] imgs_directions;

    public int type = 0;
    public int direction = 0;

    //public GameObject parent_panel;
    //public int coord_in_panel;

    GameObject back;

    void ChangeImg()
    {
        if (imgs_types.Length > type)
        {
            back.GetComponent<SpriteRenderer>().sprite = imgs_types[type];
        }
        if (imgs_directions.Length > direction)
        {
            GetComponent<SpriteRenderer>().sprite = imgs_directions[direction];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        back = transform.Find("Back").gameObject;
        //Debug.Log("Start in MoveTile");
        ChangeImg();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImg();
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log("Click in MoveTile");
    //    type = 0;

    //    if (parent_panel != null)
    //    {
    //        parent_panel.GetComponent<MovesPanel>().Click(coord_in_panel);
    //    }
    //}
}
