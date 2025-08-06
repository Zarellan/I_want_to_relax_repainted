using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using PrimeTween;
using System;



using UnityEditor;
public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update

    Dialogue dial;

    [SerializeField]
    DialogueVar[] dialVar;

    [SerializeField]
    DialogueVar[] dialVarArabSon;

    public GameObject dialo;
    

    [SerializeField]
    bool isDialogue;

    [SerializeField]
    bool forceIt;

    private bool isInside = false;

    bool entering = false;


    [SerializeField]
    string SceneToChange;

    private string sceneClean;

    bool isDialoguing = false;

    [SerializeField]
    string currentScene;


    static bool prepEnt;

    bool prepToEnter = true;

    [SerializeField]
    float posStartX;

    [SerializeField]
    float posStartY;


    [SerializeField]
    string condName;
    [SerializeField]
    bool dialogueCond;
    [SerializeField]
    bool condIsTrue = true;


    void Start()
    {

        
        prepEnt = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().canDialogue && prepToEnter && isInside) // for better experience
            prepToEnter = false;


        if(isInside)
        {
            if (!forceIt)
            {
                if (Input.GetKeyDown(KeyCode.Z) && !entering  && !GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().canDialogue && ScriptCondition(condName))
                    {
                        if (isDialogue)
                        {
                            if (!isDialoguing)
                            {
                                GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().SetDialogue(dialVar,dialVarArabSon);
                                GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().StartDialogue();
                                isDialoguing = true;
                            }
                            else
                                isDialoguing = false;

                        }
                        else
                        {
                            if (prepToEnter && !BeginDialogue.beginDialog)
                                SceneChange(SceneToChange);
                            else
                                prepToEnter = true;
                        }
                    }
            }



        }

        if (!GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().canDialogue) // for better experience
            prepToEnter = true;
    }

    

    void OnTriggerEnter2D(Collider2D other)
        {
            
                 if (GameObject.FindWithTag("PlayerCollision").name == other.name)
                    {
                        isInside = true;

                        if (forceIt  && !prepEnt && !GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().canDialogue)
                        {
                        if (isDialogue)
                        {
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().SetDialogue(dialVar,dialVarArabSon);
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().StartDialogue();
                        }
                        else
                        {

                            SceneChange(SceneToChange);
                        }
                        }

                    }


        }

            /*void OnTriggerStay2D(Collider2D other) the method isn't updating properly
                {
                    
                }*/

        void OnTriggerExit2D(Collider2D other)
        {
            if (GameObject.FindWithTag("PlayerCollision").name == other.name)
            {
                isInside = false;
                prepEnt = false;
            }

            prepToEnter = true;
        }

    
    void SceneChange(string scene)
    {

        PlayerScript.posStartX = posStartX;
        PlayerScript.posStartY = posStartY;


        
        float duration = 0.3f;
        GameObject.FindWithTag("MainFlashLight").GetComponent<FlashingLight>().FlashOut(duration);
        StartCoroutine(Timer.timer(0.5f,StartChangeScene,scene));
    }

    void StartChangeScene(string scene)
    {
        //sceneClean = AssetDatabase.GetAssetPath(SceneToChange);
        //sceneClean = System.IO.Path.GetFileNameWithoutExtension(sceneClean);
        SceneManager.LoadScene(scene);
        GameObject.FindWithTag("MainFlashLight").GetComponent<FlashingLight>().FlashIn(0.3f);
    }

        bool ScriptCondition(string name)
        {
            if (dialogueCond)
                return IsTrue(name);
            else
                return true;
        }


        bool IsTrue(string name)
        {
            if (condIsTrue)
                return StaticScript.GetCond(name);
            else
                return !StaticScript.GetCond(name);
        }

}
