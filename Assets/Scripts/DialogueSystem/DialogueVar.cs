using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class DialogueVar
{


    public string dialogue;
    public float speed;

    public AudioClip characterSound;

    public string animIconToPlay;

    public UnityEvent eventToPlay;

    public bool speak = true;

    public bool canSkip = true;

    public float timeToFinish;

    public enum Character
    {
        none,
        hamboza,
        zarellan
    }

    public Character character;

    public enum Animationo
    {
        idle,
        talk,
        talkLeft,
        talkUp,
        looking,
        happy,
        pissed,
        shocked,
        talkRight
    }

    public Animationo animation;




}
