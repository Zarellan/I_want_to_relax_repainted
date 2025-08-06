using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PrimeTween;
using TMPro;
using System;
using UnityEngine.U2D.IK;
using UnityEngine.TextCore.Text;

public class Dialogue : MonoBehaviour
{


    [SerializeField]
    GameObject dialogueSys;

    [SerializeField]
    Text text;

    [SerializeField]
    TextMeshProUGUI text2;


    [SerializeField]
    GameObject img;


    [SerializeField]
    DialogueVar[] dialogue;

    [SerializeField]
    DialogueVar[] dialogueArabSon;

    string dialogueStorage;

    float timeo = 0.05f;

    int index = 0 ;

    int dialogueCase = 0;


    string part;
    string[] remainingText;

    string[] command;

    int[] partsNum;


    string dialogueStorageClean;
    
    bool dialogueEnded = false;

    bool dialoguing = true;

    public AudioSource sound;

    public bool canDialogue = false;

    bool isAddingRich = false;

    bool instantIt = false;

    bool skipped = false;

    bool waited = false;
    //characters
    public enum Hamboza
    {
        normal,
        angry,
        idk

    }
    //
    [SerializeField]
    GameObject icon;

    [SerializeField]
    Animator iconPlay;

    private bool canSkip = true;


    int maxLengthInc = 0; //when text is added the number will increased by length of the text (for event system purpose)
    

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {



        //StartDialogue();
        //StartCoroutine(Timer.timer(timeo,Diag));

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        if (canDialogue)
        {
        if (Input.GetKeyDown(KeyCode.Z) && dialogueEnded && canSkip)
        {
            NextDiag();
        }

        if (Input.GetKeyDown(KeyCode.X) && !dialogueEnded && canSkip)
        {
            //dialoguing = true;
            Dialoguing();
            SkipDialogue();
        }
        }

        //RichChecker();

        if (instantIt)
        {
            Instant();
        }
        
        //if (skipped)
        //    Diag();

    }


    public void SetDialogue(DialogueVar[] dialo)
    {
        dialogue = dialo;
    }


    public void SetDialogue(DialogueVar[] dialo,DialogueVar[] dialoArabSon)
    {
        dialogue = dialo;
        dialogueArabSon = dialoArabSon;
    }
    public void StartDialogue()
    {
        canSkip = true;

        GameObject.FindWithTag("Player").GetComponent<PlayerScript>().Idle();

        if (!canDialogue)
        {
        GameObject.FindWithTag("Player").GetComponent<PlayerScript>().canMove = false;
        if (dialogue[dialogueCase].speak)
        dialogueSys.SetActive(true);

        dialogueEnded = false;

        dialogueCase = 0;
        text2.text = "";

        maxLengthInc = 0;

        timeo = dialogue[dialogueCase].speed;

        IconVoiceSys();
        
        CharacterAnimationPlay();
        
        StartCoroutine(Timer.timer(timeo,Diag));

        EventSystem();

        dialogue[dialogueCase].eventToPlay.Invoke();

        if (!dialogue[dialogueCase].speak || !dialogue[dialogueCase].canSkip)
        {
            canSkip = false;
        }


        }

        canDialogue = true;
    }




    void Diag()
    {


        if (text2.text.Length < dialogueStorageClean.Length + maxLengthInc)
        {
            if (dialoguing)
            {
                text2.text += dialogueStorageClean[index].ToString();
                index += 1;
                StartCoroutine(Timer.timer(timeo,Diag));
                if (!skipped)
                sound.Play();
            }
        }
        else
        {
            dialogueEnded = true;
            if (!dialogue[dialogueCase].canSkip)
                StartCoroutine(Timer.timer(dialogue[dialogueCase].timeToFinish,TimerFinishForUnskip));
        }
        


        EventChecker();
        

    }


    void PrintCommand()
    {
        
        
    }
    public Animation animIconToPlay;


    void NextDiag()
    {
        dialogueCase += 1;


        if (dialogue[dialogueCase].speak)
        {
        if (canDialogue)
        {
            maxLengthInc = 0;

            text2.text = "";
            index = 0;
            dialogueEnded = false;
            timeo = dialogue[dialogueCase].speed;

            skipped = false;

            IconVoiceSys();


            CharacterAnimationPlay();

            EventSystem();

            Diag();

            canSkip = true;
        }
        }
        else
        {
            dialogueSys.SetActive(false);
            CharacterAnimationPlay();
            StartCoroutine(Timer.timer(dialogue[dialogueCase].timeToFinish,TimerFinishForUnskip));
        }
            if (dialogueCase != 0)
                dialogue[dialogueCase].eventToPlay.Invoke();

        if (!dialogue[dialogueCase].speak || !dialogue[dialogueCase].canSkip)
        {
            canSkip = false;
        }

    }

    void DialogueFinished()
    {
        canDialogue = false;
        canSkip = true;
        index = 0;
        GameObject.FindWithTag("Player").GetComponent<PlayerScript>().canMove = true;

        dialogueSys.SetActive(false);
        //img.SetActive(false);
        dialogueCase = 0;


    }

    void changeSpeedText(float number)
    {
        if (!skipped)
        timeo = number;
    }

    void ExecuteEvent(string command)
    {
         // initializing

        try
        {
        string[] comm = command.IndexOf(',') != 1?command.Split(','):null; 

        string coma = comm.Length > 0?comm[0]:command;

        string value = comm.Length > 1?comm[1]:null; // in case parameter hasn't been added

        string value2 = comm.Length > 2?comm[2]:null; // for extra purpose

        string value3 = comm.Length > 3?comm[3]:null; // for another extra purpose

        switch(coma)
        {
            case "speed":changeSpeedText(float.Parse(value));break; // changing the text speed value
            case "print":print(value);break; // printing (for test purpose)
            case "finish it":DialogueFinished();break; // finishing the dialogue
            case "skip":SkipDialogue();break; // skipping dialogue
            case "wait":waitTime(float.Parse(value));break;// dialogue text wait's for a specific time
            //case "voice":waitTime(value);break;// change voice in specific dialogue (not made yet)
            case "camPosX":CamPosX(float.Parse(value));break;// move camera with X position
            case "camPosY":CamPosY(float.Parse(value));break; // move camera with Y position
            case "scene change":SceneChange(value,float.Parse(value2),float.Parse(value3));break;
            case "Instant":Instant();break;
            case "UnInstant":UnInstant();break;
            case "color":ColorChange(value);break;
            case "changeChar":CharacterChange(value,value2);break;
            case "zarellan flick":ZarellanFlick();break;
            case "pos character":characterPos(value,float.Parse(value2),float.Parse(value3));break;
            case "set active":SetActiveObject(value,bool.Parse(value2));break;
            case "force next":ForceNext();break;
            case "music":StartMusic(value);break;

        }
        }
        catch{}
        //print(coma);


    }


    void EventSystem() // event system to make the task easier instead of adding every statement in the script
    {
        if (PlayerPrefs.GetString("censorMode") == "Uncensored")
        dialogueStorage = dialogue[dialogueCase].dialogue; // storaging dialogue on every index
        else if (PlayerPrefs.GetString("censorMode") == "ArabSon")
        dialogueStorage = dialogueArabSon[dialogueCase].dialogue; // storaging dialogue on every index

        command = new string[10];

        partsNum = new int[10];

        remainingText = new string[10];



        //string[] parts = dialogueStorage.Split('['); // splitting every [ symbol


        /*for (int i = 0;i < parts.Length - 1;i++)
        {

            partsNum[i] = dialogueStorage.IndexOf('['); // getting dialogue storage index

            this.command[i] = parts[i + 1].Substring(0, parts[i + 1].IndexOf(']')); // getting every command before ] symbol and after [ symbol
            

            if (i == 0)
            this.remainingText[0] = dialogueStorage; // initializing
            
            string partse = "";
            for (int j = 0;j < i;j++) // getting every remained text after removing previous event symbol
            {
                partse += parts[j].Substring(parts[j].IndexOf(']') + 1);
                this.remainingText[0] = partse + remainingText[0].Substring(remainingText[0].IndexOf(']') + 1);
                
            }
            

            partsNum[i] = remainingText[0].IndexOf('['); // getting every index text length


        }

        dialogueStorageClean = ""; // resetting the dialogue storage
        for (int i = 0; i < parts.Length ; i++)
            dialogueStorageClean += parts[i].Substring(parts[i].IndexOf(']') + 1); // getting every text that doesn't contains event symbols

        for (int i = 0;i < parts.Length - 1;i++)
        {
            //print($"{i}-> part number: {partsNum[i]}\ncommand: {command[i]}");
        }*/
        RemoveRectangleString(dialogueStorage);

    }

    void SkipDialogue()// skipping dialogue (in case player don't care about the story)
    {
        //text2.text = dialogueStorageClean; removed due to event system functionality
        dialoguing = true;
        skipped = true;
        timeo = 0.0000000000000000001f;
        Diag();
        //dialogueEnded = true;
    }

    void waitTime(float waitTime)
    {
        if (!waited && !skipped)
        {
            iconPlay.Play(dialogue[dialogueCase].animIconToPlay + " End");
            UnDialoguing();
            StartCoroutine(Timer.timer(waitTime,Dialoguing));
            waited = true;
        }

    }

    void UnDialoguing()
    {
        dialoguing = false;
    }

    void Dialoguing()
    {
        dialoguing = true;
        if (waited)
        {
        Diag();
        iconPlay.Play(dialogue[dialogueCase].animIconToPlay);
        waited = false;
        }
    }


    void ColorChange(string color) // less performance but more efficient (more efficient in this case since having a event system)
    {
        string col = "";

        col = "<color="+color+">";
        maxLengthInc += col.Length;

        text2.text += col;
    }

    void ChangeAudio()
    {
        
    }

    void CharacterChange(string value1,string value2)
    {
        
        CharacterAnimationPlayAdvanced(value1,value2);

    }

    void CharacterAnimationPlay()
    {



        var dialogueia = dialogue[dialogueCase];

        if (PlayerPrefs.GetString("censorMode") == "Uncensored")
            dialogueia = dialogue[dialogueCase];
        else if (PlayerPrefs.GetString("censorMode") == "ArabSon")
            dialogueia = dialogueArabSon[dialogueCase];


        switch(dialogueia.character)
        {

            case DialogueVar.Character.hamboza:
            {
                if (dialogueia.animation == DialogueVar.Animationo.idle)
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().Play("idle");
                }

                if (dialogueia.animation == DialogueVar.Animationo.talkLeft)
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().Play("Idle left");
                }

                if (dialogueia.animation == DialogueVar.Animationo.shocked)
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().Play("shocked");
                }

                if (dialogueia.animation == DialogueVar.Animationo.talkUp)
                    GameObject.FindWithTag("Player").GetComponent<Animator>().Play("Idle up");

                

            };break;
            case DialogueVar.Character.zarellan:
            {
                if (dialogueia.animation == DialogueVar.Animationo.idle)
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("Idle");
                }

                if (dialogueia.animation == DialogueVar.Animationo.looking)
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("looking");
                }

                if (dialogueia.animation == DialogueVar.Animationo.talkRight)
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("look 1");
                } 
                if (dialogueia.animation == DialogueVar.Animationo.shocked)
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("Shocked");
                }
                if (dialogueia.animation == DialogueVar.Animationo.pissed)
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("angry");
                }
                if (dialogueia.animation == DialogueVar.Animationo.happy)
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("happy");
                }


            }break;
        }


    }


    void characterPos(string name,float x,float y)
    {
        switch(name)
        {
            case "hamboza":GameObject.FindWithTag("Player").transform.position = new Vector3(x,y);break;
            case "zarellan":GameObject.FindWithTag("Player2").transform.position = new Vector3(x,y);break;
        }

    }

    public void CharacterPosEvent(string text)
    {
        string[] texts = text.Split(',');

        switch(texts[0])
        {
            case "hamboza":GameObject.FindWithTag("Player").transform.position = new Vector3(float.Parse(texts[1]),float.Parse(texts[2]));break;
            case "zarellan":GameObject.FindWithTag("Player2").transform.position = new Vector3(float.Parse(texts[1]),float.Parse(texts[2]));break;
        }

    }


    void SetActiveObject(string name,bool value)
    {
        
        GameObject.FindWithTag(name).SetActive(value);
        

    }

    public void SetActiveObjectEvent(string text)
    {
        string[] texts = text.Split(',');

        GameObject.FindWithTag(texts[0]).SetActive(bool.Parse(texts[1]));
    }

    public void ObjectAppereance(string text)
    {
        string[] texts = text.Split(',');

        Color col = texts[1] == "invisible"?new Color(255,255,255,0):new Color(255,255,255,1);

        GameObject.FindWithTag(texts[0]).GetComponent<SpriteRenderer>().color = col;
    }

    void ForceNext()
    {
        //SkipDialogue();
        NextDiag();

    }


    void CharacterAnimationPlayAdvanced(string character,string anim)
    {



        switch(character)
        {

            case "hamboza":
            {
                if (anim == "idle")
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().Play("idle");
                }

                if (anim == "idle left")
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().Play("Idle left");
                }
                

            };break;
            case "zarellan":
            {
                if (anim == "idle")
                {
                    GameObject.FindWithTag("Player").GetComponent<Animator>().Play("Idle");
                }

                if (anim == "looking")
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("looking");
                }
                if (anim == "angry")
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("angry");
                }
                if (anim == "shocked")
                {
                    GameObject.FindWithTag("Player2").GetComponent<Animator>().Play("Shocked");
                }


            }break;
        }


    }


    void CamPosX(float x)
    {
        GameObject cam = GameObject.FindWithTag("MainCamera");
        cam.transform.position = new Vector2(x,cam.transform.position.y);
    }

    void CamPosY(float y)
    {
        GameObject cam = GameObject.FindWithTag("MainCamera");
        cam.transform.position = new Vector2(cam.transform.position.x,y);
    }

    void SceneChange(string sceneName)
    {
        float duration = 0.5f;
        GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>().color = Color.black;
        Color ab = GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>().color;
        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),endValue:0,duration:00000.1f,Ease.Linear);
        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),endValue:1f,duration:duration,Ease.Linear);
        StartCoroutine(Timer.timer(duration,StartChangeScene,sceneName));
    }

    void SceneChange(string sceneName,float x,float y)
    {
        PlayerScript.posStartX = x;
        PlayerScript.posStartY = y;

        float duration = 0.5f;
        GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>().color = Color.black;
        Color ab = GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>().color;
        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),endValue:0,duration:00000.1f,Ease.Linear);
        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),endValue:1f,duration:duration,Ease.Linear);
        StartCoroutine(Timer.timer(duration,StartChangeScene,sceneName));
    }

    void TimerFinishForUnskip()
    {
        dialogueSys.SetActive(true);

        NextDiag();
    }


    public void ZarellanFlick()
    {
        GameObject.FindWithTag("MainFlashLight").GetComponent<FlashingLight>().FlickEffect();
    }

    public void CardTrans(string text)
    {
        string[] textso = text.Split(',');

        GameObject.FindWithTag("MainFlashLight").GetComponent<FlashingLight>().CardTrans(textso[0],float.Parse(textso[1]));

        print("text: "+textso[0]+"\n number: "+textso[1]);
    }

    void StartChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Tween.Alpha(GameObject.FindWithTag("FlashLight").GetComponent<SpriteRenderer>(),endValue:0,duration:0.5f,Ease.Linear);
    }

    public int GetCaseDialogue()
    {
        return dialogueCase;
    }

    void Instant()
    {
        instantIt = true;
        if (instantIt)
        {
        //text2.text += dialogueStorageClean[index].ToString();
        //index += 1;
        Diag();
        }


        EventChecker();
    }

    void UnInstant()
    {
        instantIt = false;
    }

    public void FinishIconAnimation()
    {

        //float length = iconPlay.GetCurrentAnimatorStateInfo(0).length;
        //float normalize = iconPlay.GetCurrentAnimatorStateInfo(0).normalizedTime;
        

        if (dialogueEnded && dialogue[dialogueCase].animIconToPlay != "" /*&& normalize % length > 0.8 && dialogue[dialogueCase].animIconToPlay != ""*/)
        {
            iconPlay.Play(dialogue[dialogueCase].animIconToPlay + " End");
        }


    }



    public void IconVoiceSys()
    {
        if (dialogue[dialogueCase].animIconToPlay == "")
        {
            icon.SetActive(false);
            text2.rectTransform.anchoredPosition = new Vector3(85f,text.rectTransform.anchoredPosition.y);
            text2.rectTransform.sizeDelta = new Vector3(1324.4f,text.rectTransform.sizeDelta.y);
        }
        else
        {
            icon.SetActive(true);
            text2.rectTransform.anchoredPosition = new Vector3(222.6f,text.rectTransform.anchoredPosition.y);
            text2.rectTransform.sizeDelta = new Vector3(1205.9f,text.rectTransform.sizeDelta.y);
        }

        if (PlayerPrefs.GetString("censorMode") == "Uncensored")
        {
            sound.clip = dialogue[dialogueCase].characterSound;
            if (dialogue[dialogueCase].animIconToPlay != "")
            iconPlay.Play(dialogue[dialogueCase].animIconToPlay);
        }
        else if (PlayerPrefs.GetString("censorMode") == "ArabSon")
        {
            sound.clip = dialogueArabSon[dialogueCase].characterSound;
            if (dialogue[dialogueCase].animIconToPlay != "")
            iconPlay.Play(dialogue[dialogueCase].animIconToPlay);
        }

    }

    public void StartMusic(string name)
    {
        GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().ChangeMusic(name);
    }

    public void StartMusicVol(string name)
    {
        string[] texts = name.Split(',');
        GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().ChangeMusic(texts[0],float.Parse(texts[1]));
    }
    public void StopMusic()
    {
        GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().StopMusic();
    }
    public void PauseMusic()
    {
        GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().PauseMusic();
    }

    public void UnpauseMusic()
    {
        GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().UnpauseMusic();
    }

    public void RichChecker() // failed
    {
        
        if (dialogueStorageClean[index] == '<' && dialogueStorageClean[index] != '>' || isAddingRich)
        {
            isAddingRich = true;
            text2.text += dialogueStorageClean[index].ToString();
            index += 1;
            
            


            if (dialogueStorageClean[index] == '>')
            {

                text2.text += dialogueStorageClean[index].ToString();
                index += 1;
                
                isAddingRich = false;
            }
        }

    }


    void EventChecker()
    {
        for (int i = 0; i < partsNum.Length;i++) // looping on every parts number length that has been added in the array
        {
            if (text2.text.Length - maxLengthInc == partsNum[i] && dialogue[dialogueCase].dialogue != "" ) // activate when the text is on the specific requested length
                ExecuteEvent(command[i]); // execute the event
        }

    }

    public void CamChange(string text)
    {
        string[] texts = text.Split(',');
        Camera cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cam.transform.position = new Vector3(float.Parse(texts[0]),float.Parse(texts[1]),-10);
        cam.orthographicSize = float.Parse(texts[2]);

    }

    public void PlayAudio(string text)
    {
        GameObject.FindWithTag("MusicPlayer").GetComponent<MusicPlayer>().PlaySound(text);
    }


    void RemoveRectangleString(string text)// probably solved (will be back)
    {
        //string hello = "hii[dasdas] how[sasa] susssy dasdasas [dsa] lol [hamboooza] hehhe I lk";

        string[] helloSplit = text.Split('[');

        string[] helloSplit2 = text.Split(']');

        //int[] partsNum;
        //partsNum = new int[30];

        //string[] commands;
        //commands = new string[30];

        string remainingText = "";

        string remainingText2 = text;
        
        for (int i = 0;i < helloSplit.Length-1;i++)
        {
            
            this.partsNum[i] = remainingText2.IndexOf('[');
            remainingText += CheckIfExist(helloSplit2[i+1],'[');
            this.command[i] = helloSplit[i+1].Substring(0,helloSplit[i+1].IndexOf(']'));

            remainingText2 = remainingText2.Substring(0,remainingText2.IndexOf('[')) + remainingText2.Substring(remainingText2.IndexOf(']') + 1);

        }
        //this.partsNum = partsNum;
        //this.command = command;
        this.dialogueStorageClean = remainingText2;
        

    }



    public string CheckIfExist(string thisArray,char condit)
    {
        if (thisArray.IndexOf(condit) != -1)
        {
            return thisArray.Substring(0,thisArray.IndexOf(condit));
        }
        else
            return thisArray.Substring(0);
    }


    public void PrintTest()
    {
        print("omak1");
    }
    
}


