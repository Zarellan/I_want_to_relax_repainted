using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDialogue : MonoBehaviour // didn't know what to name it
{


    Dialogue dial;

    [SerializeField]
    DialogueVar[] dialVar;

    [SerializeField]
    DialogueVar[] dialVarArabSon;
    

    [SerializeField]
    bool forceIt;

    private bool isInside = false;

    private string objectName;

    [SerializeField]
    bool dialogueCond = true;
    [SerializeField]
    bool changeCond = true;
    [SerializeField]
    bool condIsTrue;
    [SerializeField]
    string condName;


    bool isDialoguing = false; // this boolean exist for better compatibility

    Dialogue theDialogue;


    void Start()
    {
        theDialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
        StartCoroutine(Timer.timer(1,()=> {theDialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();} ));
    }

    void Update()
    {
                theDialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();

        if (isInside && !forceIt && ScriptCondition(condName)) // when collide the object and isn't in force mode
        {
                if (Input.GetKeyDown(KeyCode.Z) && !theDialogue.canDialogue) // when pressed Z and dialogue is finished
                    {
                        if (!isDialoguing) // reason this bool exist: in worst cases -> unity's function order is dialogue then this.gameobject
                        {
                            if (dialVarArabSon != null)
                            dialIt(dialVar,dialVarArabSon); // dialogue will be activated
                            else
                            dialIt(dialVar,dialVar);

                            isDialoguing = true;

                            if (dialogueCond && changeCond)
                            {
                                StaticScript.FinishIt(condName);
                            }


                        }
                        else
                            isDialoguing = false;
                    }
        }


    }


    void OnTriggerEnter2D(Collider2D other)
        {
                 if (GameObject.FindWithTag("PlayerCollision").name == other.name)
                    {
                        isInside = true;

                        if (forceIt) // if statement is in force mode, the dialogue will be activated instantly
                        {
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().SetDialogue(dialVar,dialVarArabSon);
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().StartDialogue();
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
            }

        }

        void dialIt(DialogueVar[] dial,DialogueVar[] dialArabSon)
        {
            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().SetDialogue(dialVar,dialVarArabSon);
            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().StartDialogue();

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
