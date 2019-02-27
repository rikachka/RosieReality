using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public enum CounterType { EMPTY, AVAILABLE }

    public Sprite[] imgs;

    public int img_type = 0;

    public int number_max = 0;
    public int number_available = 0;

    public GameObject counter;
    GameObject[] counter_panel;

    float step_factor = 0.5F;
    float shift = 1;

    void ChangeImg()
    {
        if (imgs.Length > img_type)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[img_type];
        }

        Vector3 left_top_coords = transform.position;
        counter_panel = new GameObject[number_max];

        for (int x = 0; x < number_available; x++)
        {
            counter_panel[x] = Instantiate(counter);
            counter_panel[x].GetComponent<Counter>().img_type = (int)CounterType.EMPTY;
            counter_panel[x].transform.position = new Vector3(left_top_coords.x + shift + x * step_factor, left_top_coords.y, left_top_coords.z);
        }

        for (int x = number_available; x < number_max; x++)
        {
            counter_panel[x] = Instantiate(counter);
            counter_panel[x].GetComponent<Counter>().img_type = (int)CounterType.AVAILABLE;
            counter_panel[x].transform.position = new Vector3(left_top_coords.x + shift + x * step_factor, left_top_coords.y, left_top_coords.z);
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

    }
}
