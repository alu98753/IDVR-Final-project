using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Poster;
    public GameObject displayPrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            findPoster();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddDisPlay();
        }
    }

    public void findPoster()
    {
        Poster = GameObject.FindGameObjectsWithTag("Poster");
    }

    public void AddDisPlay()
    {
        foreach (GameObject poster in Poster)
        {
            Instantiate(displayPrefab, transform.position + transform.forward * 2, Quaternion.identity, poster.transform);
        }
    }
}
