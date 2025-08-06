using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RepaintScript : MonoBehaviour
{

    //Animation anim;


    [SerializeField]
    Animation dustAnimate;
    
    [SerializeField]
    Animator[] dustAnimo;

    [SerializeField]
    new AudioSource audio;

    [SerializeField]
    AudioClip pizzaTower;

    [SerializeField]
    AudioClip ruinOpen;

    [SerializeField]
    AudioSource beforeStory;

    public ButtonsPosition[] button;

    public Text dropdownText;

    public GameObject mainOptions;
    public OptionObject[] options;

    public Button backButton;
    public Text backText;

    public Text optionText;

    public bool canOption = true;

    public bool gameStarting = false;

    public FlashingLight flash;


    public Dropdown censor;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("censorMode")/*PlayerPrefs.GetString("censorMode") != null || PlayerPrefs.GetString("censorMode") != ""*/)
            switch(PlayerPrefs.GetString("censorMode"))
            {
                case "Uncensored":censor.value = 0;break;
                case "ArabSon":censor.value = 1;break;
            }
        else
            PlayerPrefs.SetString("censorMode","Uncensored");
        

        
        //anim = GetComponent<Animation>();
        for (int i = 0;i< options.Length;i++)
        {
            if (options[i].optionImage == null)
            {
                options[i].text.CrossFadeAlpha(0, 0.0001f, false);
            }
            else
                Tween.Alpha(options[i].optionImage,endValue:0,duration:0.0001f,Ease.Linear);


        }


                backText.CrossFadeAlpha(0, 0.0001f, false);

        flash = new FlashingLight();

        mainOptions.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void DustIt()
    {
        //dustAnimate.playAutomatically = false;
        // dustAnimate.Play("dustEffect");
        for (int i = 0; i < dustAnimo.Length;i++)
        dustAnimo[i].SetBool("startEffect",true);

        audio.clip = pizzaTower;
        audio.Play();
        
    }   


    void MenuAppear()
    {
        //Tween.position();
        //Tween.PositionX();
        for (int i = 0; i < button.Length;i++)
        Tween.PositionX(button[i].button.transform,button[i].buttonPos,duration:0.6f,Ease.OutSine);

        beforeStory.Play();
    }


    public void StartIt()
    {
        if (!gameStarting)
        {
        gameStarting = true;

        flash.FlashOut(5,Color.white);

        beforeStory.Stop();

        audio.clip = ruinOpen;
        audio.Play();

        StartCoroutine(Timer.timer(5,SceneChange));
        }

    }
    
    public void Quit()
    {
        if (!gameStarting)
        Application.Quit();
    }

    public void Options()
    {
        if (canOption && !gameStarting)
        {
        mainOptions.SetActive(true);
        for (int i = 0;i< options.Length;i++)
        {
            if (options[i].optionImage == null)
            {
                //options[i].text.CrossFadeAlpha(1.0f, 1f, false);
                DeHideButton(options[i].text);
            }
            else
                Tween.Alpha(options[i].optionImage,endValue:1,duration:1,Ease.Linear);




        }

        for (int i = 0; i < button.Length - 2;i++)
            Tween.PositionX(button[i].button.transform,button[i].previousPosX,duration:0.6f,Ease.InSine);

        button[3].button.SetActive(true);
        DeHideButton(backText);
        optionText.CrossFadeAlpha(0, 1f, false);
        StartCoroutine(Timer.timer(1,HideOption));


        canOption = false;
        }

    }

    public void DeOption()
    {
        

        if (canOption  && !gameStarting)
        {
            for (int i = 0;i< options.Length;i++)
            {
                if (options[i].optionImage == null)
                {
                    options[i].text.CrossFadeAlpha(0f, 1f, false);
                }
                else
                    Tween.Alpha(options[i].optionImage,endValue:0,duration:1,Ease.Linear);




            }

            for (int i = 0; i < button.Length - 2;i++)
                Tween.PositionX(button[i].button.transform,button[i].buttonPos,duration:0.6f,Ease.InSine);



            button[2].button.SetActive(true);
            backText.CrossFadeAlpha(0, 1f, false);
            DeHideButton(optionText);
            StartCoroutine(Timer.timer(1,HideDeOption));

            canOption = false;
        }

    }


    public void OptionsCensor()
    {
        
        if (dropdownText.text == "<color=red>Uncensored</color>")
            PlayerPrefs.SetString("censorMode","Uncensored");
        else if (dropdownText.text == "Arab Son")
            PlayerPrefs.SetString("censorMode","ArabSon");

    }


    void HideOption()
    {
        canOption = true;
        button[2].button.SetActive(false);
    }

    void HideDeOption()
    {
        canOption = true;
        button[3].button.SetActive(false);
        mainOptions.SetActive(false);
    }


    void DeHideButton(Text tex)
    {
            tex.CrossFadeAlpha(0, 0.00001f, false);
            tex.CrossFadeAlpha(1, 1f, false);

    }


    void SceneChange()
    {
        flash.FlashIn(2,Color.white);
        SceneManager.LoadScene("HambozaRoom");
    }

}
