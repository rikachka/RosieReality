using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTileClick : MonoBehaviour
{
    public GameObject parent_panel;
    public int coord_in_panel;

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
        if (parent_panel != null)
        {
            parent_panel.GetComponent<MovesPanel>().Click(coord_in_panel);
        }
    }
}
