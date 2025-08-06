using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{


    GameObject flashLight;

    public SpriteRenderer flashLightRend;


    GameObject soundRend;

    [SerializeField]
    AudioClip soundFlick;

    [SerializeField]
    TMP_Text textObj;

    // Start is called before the first frame update

    void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        flashLight = GameObject.FindWithTag("FlashLight");

        flashLightRend = flashLight.GetComponent<SpriteRenderer>();
        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    public void FlashOut(float duration,Color color)
    {
        ChangeColor(color,0);
        
        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),1,duration,Ease.Linear);
    }

        public void FlashOut(float duration)
    {
        ChangeColor(Color.black,0);

        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),1,duration,Ease.Linear);
    }


    public void FlashIn(float duration, Color color)
    {
        ChangeColor(color,1);

        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),0,duration,Ease.Linear);
    }

    public void FlashIn(float duration)
    {
        ChangeColor(Color.black,1);

        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),0,duration,Ease.Linear);
    }

    public void FlickEffect()
    {
        ChangeColor(Color.black,1);
        StartCoroutine(Timer.timer(0.2f,FlickOut));

        GetComponent<AudioSource>().clip = soundFlick;
        GetComponent<AudioSource>().Play();
        //soundRend = Instantiate(gameObject);
        //soundRend.AddComponent<AudioSource>();
        //soundRend.GetComponent<SpriteRenderer>().enabled = false;

    }


    void FlickOut()
    {
        ChangeColor(Color.black,0);
        
        GetComponent<AudioSource>().Play();

    }



    public void ChangeColor(Color color,float opacity)
    {
        Color col = GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>().color;
        col = color;
        col.a = opacity;
        GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>().color = col;
    }


    public void CardTrans(string text,float duration)
    {
        ChangeColor(Color.black,1);
        textObj.text = text;
        StartCoroutine(Timer.timer(duration,()=>{textObj.text = "";ChangeColor(Color.black,0);}));
    }

}
