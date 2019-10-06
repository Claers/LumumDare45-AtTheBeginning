using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public Game gc;
    public VideoPlayer videoPlayer;
    public List<VideoClip> vClipsFR;
    public List<VideoClip> vClipsEN;

    // Start is called before the first frame update
    void Start()
    {
        gc = GetComponent<Game>();
    }

    public void PlayVideo(int nb)
    {
        videoPlayer.Stop();
        if (gc.isEn)
        {
            videoPlayer.clip = vClipsEN[nb];
        }
        else
        {
            videoPlayer.clip = vClipsFR[nb];
        }
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
