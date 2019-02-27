﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClick : MonoBehaviour
{
    public GameObject parent;

    public int x, y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void OnMouseDown()
    {
        if (parent != null)
        {
            parent.GetComponent<Field>().Click(x, y);
        }
    }
}
