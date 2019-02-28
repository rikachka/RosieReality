using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTileClick : MonoBehaviour
{
    public GameObject parent;

    public Point parent_point;

    private void OnMouseDown()
    {
        if (parent != null)
        {
            parent.GetComponent<Field>().Click(parent_point);
        }
    }
}
