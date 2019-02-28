using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangerTileClick : MonoBehaviour
{
    public GameObject parent;
    public int index;

    private void OnMouseDown()
    {
        if (parent != null)
        {
            parent.GetComponent<Exchanger>().Click(index);
        }
    }
}
