using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTileClick : MonoBehaviour
{
    public GameObject parent;
    public int parent_index;

    private void OnMouseDown()
    {
        if (parent != null)
        {
            parent.GetComponent<MovesPanel>().Click(parent_index);
        }
    }
}
