using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Poster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject product;
    public Author author;
    public GameObject user;
    public float distance;
    void Start()
    {
        //product.SetActive(false);

        user = GameObject.FindWithTag("Player");
        print("user "+user);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Z))
        // {
        //     ShowProduct();
        // }
        if (user != null && UnityEngine.Vector3.Distance(user.transform.position, transform.position) < distance)
        {
            author.gameObject.SetActive(true);
        }
        else if(author!=null&&author.isActiveAndEnabled)
        {
            author.leave();
        }

    }

    public void ShowProduct()
    {
        product.SetActive(true);
    }
}
