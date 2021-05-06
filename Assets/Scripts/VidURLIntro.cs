using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidURLIntro : MonoBehaviour
{
    VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        if (video)
        {
            video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Alive-Intro Boi Edition.mov");
            video.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
