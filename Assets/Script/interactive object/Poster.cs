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
    bool IsInteract = false;

    public float distance;
    void Start()
    {
        user = GameObject.Find("CenterEyeAnchor");
        //print("user "+user);
    }

    // Update is called once per frame
    void Update()
    {
        //print(UnityEngine.Vector3.Distance(user.transform.position, transform.position));
        if (user != null && UnityEngine.Vector3.Distance(user.transform.position, transform.position) < distance)
        {
            PosterPlane.SetActive(true);
            InteractButton.SetActive(true);

        }
        else if (author != null && author.isActiveAndEnabled)
        {
            //print("not in range");
            author.leave();
            //PosterPlane.GetComponent<MeshRenderer>().material = Material;
            PosterPlane.SetActive(false);
            InteractButton.SetActive(false);
        }
        else if (user != null && UnityEngine.Vector3.Distance(user.transform.position, transform.position) > distance)
        {

            PosterPlane.SetActive(false);
            InteractButton.SetActive(false);
        }

    }
    public void InteractStart()
    {
        if (!displaySet.activeSelf)
        {
            displaySet.SetActive(true);
            PosterPlane.GetComponent<PDFViewer>().initPaper();
            InteractButton.SetActive(false);

        }
        else
        {
            author.leave();
        }
    }

}
