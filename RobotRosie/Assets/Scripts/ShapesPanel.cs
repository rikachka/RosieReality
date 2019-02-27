﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesPanel : MonoBehaviour
{
    struct ShapeInfo
    {
        public ShapeType shape_type;
        public int number_max; 
        public int number_available;

        public ShapeInfo(ShapeType shape_type_, int number_max_, int number_available_)
        {
            shape_type = shape_type_;
            number_max = number_max_;
            number_available = number_available_;
        }
    }

    public enum ShapeType { FORWARD, LEFT, RIGHT }

    public GameObject shape;

    GameObject[] panel;

    ShapeInfo[] shapes_info =
    {
        new ShapeInfo(ShapeType.FORWARD, 5, 3),
        new ShapeInfo(ShapeType.LEFT, 6, 2),
        new ShapeInfo(ShapeType.RIGHT, 4, 4)
    };

    int size = 3;
    float step_factor = 1.1F;

    void CreateShapesPanel()
    {
        Vector3 left_top_coords = transform.position;
        panel = new GameObject[size];

        for (int y = 0; y < size; y++)
        {
            panel[y] = Instantiate(shape);
            panel[y].GetComponent<Shape>().img_type = (int)shapes_info[y].shape_type;
            panel[y].GetComponent<Shape>().number_max = shapes_info[y].number_max;
            panel[y].GetComponent<Shape>().number_available = shapes_info[y].number_available;
            panel[y].transform.position = new Vector3(left_top_coords.x, left_top_coords.y - y * step_factor, left_top_coords.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateShapesPanel();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
