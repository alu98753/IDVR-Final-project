using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
           // findPoster();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
           // AddDisPlay();
        }
    }

    public void findPoster()
    {
        Poster = GameObject.FindGameObjectsWithTag("Poster");
    }

    public void AddDisPlay()
    {
        //foreach (GameObject poster in Poster)
        {

            print("POSITION "+Poster[1].transform.position);
            print("FORWARD "+Poster[1].transform.forward * 2);
            print("DISPLAY "+Poster[1].transform.position + Poster[1].transform.forward * 2);
            GameObject display = Instantiate(displayPrefab, Poster[1].transform.position + Poster[1].transform.forward * 2, Quaternion.identity);
            
        }
    }
}
