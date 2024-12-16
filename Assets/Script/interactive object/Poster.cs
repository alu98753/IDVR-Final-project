using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Poster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject displaySet;
    public Author author;
    public GameObject user;
    public GameObject InteractButton;
    public GameObject PosterPlane;
    public Material Material;

    public float distance;
    void Start()
    {
        user = GameObject.FindWithTag("Player");
        print("user "+user);
    }

    // Update is called once per frame
    void Update()
    {
        if (user != null && UnityEngine.Vector3.Distance(user.transform.position, transform.position) < distance)
        {
            InteractButton.SetActive(true);

        }
        else if(author!=null&&author.isActiveAndEnabled)
        {
            print("not in range");
            author.leave();
            PosterPlane.GetComponent<MeshRenderer>().material = Material;
            InteractButton.SetActive(false);
        }

    }
    public void InteractStart()
    {
        displaySet.SetActive(true);
        PosterPlane.GetComponent<PDFViewer>().initPaper();
        InteractButton.SetActive(false);
    }

}
