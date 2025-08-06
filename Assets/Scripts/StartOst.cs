using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOst : MonoBehaviour
{


    MusicPlayer music;

    [SerializeField]
    string musicName;
    [SerializeField]
    bool force;
    // Start is called before the first frame update
    void Start()
    {
        //music = GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>();
        //music.ChangeMusic(musicName);
        if (force)
        StartCoroutine(Timer.timer(0.01f,() => GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().ChangeMusic(musicName)));
        else
        {
            if (!GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().IsPlaying())
                StartCoroutine(Timer.timer(0.01f,() => GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().ChangeMusic(musicName)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
