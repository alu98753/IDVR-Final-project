using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject product;
    void Start()
    {
        product.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ShowProduct();
        }
    }

    public void ShowProduct()
    {
        product.SetActive(true);
    }
}
