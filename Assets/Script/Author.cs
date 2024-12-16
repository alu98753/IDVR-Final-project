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
    public PDFViewer viewer;

    //以下為poster新增內容
    // 新增影片、聲音清單
    public VideoPlayer videoPlayer;
    public List<VideoClip> videoClips;
    public List<AudioClip> audioClips;
    public bool JustPlayNext = false;
    private int currentIndex = 0; // 當前播放索引

    void Start()
    {

        user = GameObject.Find("CenterEyeAnchor"); ;
        animator = GetComponent<Animator>();

        //不播mp4聲音
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

        // 初始化 VideoPlayer 和 AudioSource
        //if (videoClips.Count > 0)
        //{
        //    videoPlayer.clip = videoClips[currentIndex];
        //}
        //if (audioClips.Count > 0)
        //{
        //    voice.clip = audioClips[currentIndex];
        //    mouth.clip = voice.clip;
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //talking();
            PlayNext(); // 播放下一段影片

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            stopTalking();
        }
        //print("author update");

        transform.LookAt(new Vector3(user.transform.position.x, transform.position.y, user.transform.position.z));
    }

    public void PlayNext()
    {
        print("playnext");
        // 停止當前播放
        if (!JustPlayNext)
        {
            print("playnext2");
            stopTalking();

            // 切換到下一段影片和聲音
            videoPlayer.clip = videoClips[currentIndex];
            voice.clip = audioClips[currentIndex];
            mouth.clip = audioClips[currentIndex];
            currentIndex = (currentIndex + 1) % videoClips.Count; // 循環播放

            // 開始播放影片和聲音
            talking();
            JustPlayNext = true;
            StartCoroutine(WaitForSecondsPlayNext(3));
        }
    }

    public void PlayPrev()
    {
        // 停止當前播放
        if (!JustPlayNext)
        {
            stopTalking();

            // 切換到下一段影片和聲音
            currentIndex = (currentIndex - 1);
            if (currentIndex < 0) currentIndex += videoClips.Count; // 循環播放
            videoPlayer.clip = videoClips[currentIndex];
            voice.clip = audioClips[currentIndex];
            mouth.clip = audioClips[currentIndex];
            // 開始播放影片和聲音
            talking();
            JustPlayNext = true;
            StartCoroutine(WaitForSecondsPlayNext(3));
        }
    }

    public void talking()
    {
        print("talking");
        voice.Play();
        mouth.Play();
        animator.SetBool("talking", true);

        // Poster video
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.time = 0; // 重置播放時間到起點
            videoPlayer.Play();



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
        Debug.Log("Waited for " + N + " seconds.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
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
        GameObject[] papers = GameObject.FindGameObjectsWithTag("Paper");
        foreach (GameObject paper in papers)
        {
            Destroy(paper);
        }

        stopTalking();
        animator.SetTrigger("bye");
        StartCoroutine(WaitForSecondsBye(4.73f));
    }
    public IEnumerator WaitForSecondsBye(float N)
    {
        yield return new WaitForSeconds(N);
        viewer.currentPageIndex = 0;
        currentIndex = 0;
        displaySet.SetActive(false);
        Debug.Log("Waited for " + N + " seconds.");
    }

    public IEnumerator WaitForSecondsPlayNext(float N)
    {
        yield return new WaitForSeconds(N);
        JustPlayNext = false;
        Debug.Log("Waited for " + N + " seconds.");
    }
}
