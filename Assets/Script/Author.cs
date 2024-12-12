using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Author : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource voice, mouth;
    public Animator animator;

    public GameObject user;
    public GameObject displaySet;


    //�H�U��poster�s�W���e
    // �s�W�v���B�n���M��
    public VideoPlayer videoPlayer;
    public List<VideoClip> videoClips; 
    public List<AudioClip> audioClips;
    private int currentIndex = 0; // ��e�������

    void Start()
    {
        user = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();

        //����mp4�n��
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

        // ��l�� VideoPlayer �M AudioSource
        if (videoClips.Count > 0)
        {
            videoPlayer.clip = videoClips[currentIndex];
        }
        if (audioClips.Count > 0)
        {
            voice.clip = audioClips[currentIndex];
            mouth.clip = voice.clip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //talking();
            PlayNext(); // ����U�@�q�v��

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            stopTalking();
        }
        print("author update");
        
        transform.LookAt(new Vector3(user.transform.position.x,transform.position.y,user.transform.position.z));
    }

    public void PlayNext()
    {
        // �����e����
        stopTalking();

        // ������U�@�q�v���M�n��
        currentIndex = (currentIndex + 1) % videoClips.Count; // �`������
        videoPlayer.clip = videoClips[currentIndex];
        voice.clip = audioClips[currentIndex];

        // �}�l����v���M�n��
        talking();
    }

    public void talking()
    {
        voice.Play();
        mouth.Play();
        animator.SetBool("talking", true);

        // Poster video
        if (videoPlayer != null) 
        {
            videoPlayer.Stop(); 
            videoPlayer.time = 0; // ���m����ɶ���_�I
            videoPlayer.Play();

            //��μv�����ױ���ʵe�ɶ�
            float duration = Mathf.Max((float)videoPlayer.clip.length, voice.clip.length);
            StartCoroutine(WaitForSecondsCoroutine(duration));

        }
        else
        {
            Debug.Log("NO videoPlayer.");
        }

        //StartCoroutine(WaitForSecondsCoroutine(voice.clip.length));
    }


    public IEnumerator WaitForSecondsCoroutine(float N)
    {
        yield return new WaitForSeconds(N);
        animator.SetBool("talking", false);
        Debug.Log("Waited for " + N + " seconds.");
    }

    public void stopTalking()
    {
        print("stop talking");
        voice.Stop();
        mouth.Stop();
        animator.SetBool("talking", false);

        // Poster video
        if (videoPlayer != null) 
        {
            videoPlayer.Stop(); 
            videoPlayer.time = 0; 

        }

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
        displaySet.SetActive(false);
        Debug.Log("Waited for " + N + " seconds.");
    }
}
