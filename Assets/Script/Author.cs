using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Author : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource voice, mouth, teeth;
    public Animator animator;

    public GameObject user, userEyes;

    public GameObject head;
    void Start()
    {
        user = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            talking();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            stopTalking();
        }
        print("author update");
        
        transform.LookAt(new Vector3(user.transform.position.x,transform.position.y,user.transform.position.z));
        head.transform.LookAt(userEyes.transform.position);
    }

    public void talking()
    {
        voice.Play();
        mouth.Play();
        teeth.Play();
        animator.SetBool("talking", true);
        StartCoroutine(WaitForSecondsCoroutine(voice.clip.length));
    }


    public IEnumerator WaitForSecondsCoroutine(float N)
    {
        yield return new WaitForSeconds(N);
        animator.SetBool("talking", false);
        Debug.Log("Waited for " + N + " seconds.");
    }

    public void stopTalking()
    {
        voice.Stop();
        mouth.Stop();
        teeth.Stop();
        animator.SetBool("talking", false);
    }
    public void leave()
    {
        stopTalking();
        animator.SetTrigger("bye");
        StartCoroutine(WaitForSecondsBye(4.73f));
    }
    public IEnumerator WaitForSecondsBye(float N)
    {
        yield return new WaitForSeconds(N);
        this.gameObject.SetActive(false);
        Debug.Log("Waited for " + N + " seconds.");
    }
}
