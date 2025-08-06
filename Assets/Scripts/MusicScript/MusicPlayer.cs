using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip[] soundTracks;

    [SerializeField]
    AudioClip[] sounds;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeMusic(string name)
    {

        for (int i = 0;i < soundTracks.Length; i++)
        {
            if (name == soundTracks[i].name)
            {
                GetComponent<AudioSource>().clip = soundTracks[i];
                GetComponent<AudioSource>().volume = 1;
                GetComponent<AudioSource>().Play();
            }
        }

    }

    public bool IsPlaying()
    {
        return GetComponent<AudioSource>().isPlaying;
    }

    public void ChangeMusic(string name,float volume)
    {

        for (int i = 0;i < soundTracks.Length; i++)
        {
            if (name == soundTracks[i].name)
            {
                GetComponent<AudioSource>().clip = soundTracks[i];
                GetComponent<AudioSource>().volume = volume;
                GetComponent<AudioSource>().Play();
            }
        }

    }

    public void PauseMusic()
    {
        GetComponent<AudioSource>().Pause();            
    }

    public void UnpauseMusic()
    {
        GetComponent<AudioSource>().UnPause();            
    }


    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void PlaySound(string name)
    {
        
        for (int i = 0;i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
                GetComponent<AudioSource>().PlayOneShot(sounds[i]);
        }
    }
}
