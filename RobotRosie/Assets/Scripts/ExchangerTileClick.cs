using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangerTileClick : MonoBehaviour
{
    public GameObject parent;
    public int parent_index;

    private void OnMouseDown()
    {
        if (parent != null)
        {
            parent.GetComponent<Exchanger>().Click(parent_index);
        }
    }
}
