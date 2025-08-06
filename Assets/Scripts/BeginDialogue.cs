using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BeginDialogue : MonoBehaviour
{

    [SerializeField]
    DialogueVar[] dialVar;

    [SerializeField]
    DialogueVar[] dialVarArabSon;

    [SerializeField]
    string condName;


    Dialogue theDialogue;

    [SerializeField]
    float whenStart;


    public static bool beginDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        if (ScriptCondition(condName))
        {
        GameObject.FindWithTag("Player").GetComponent<PlayerScript>().canMove = false;

        StartCoroutine(Timer.timer(whenStart,beginIt));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void beginIt()
    {
        if (ScriptCondition(condName))
        {
            beginDialog = true;
            theDialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();


            if (dialVarArabSon.Length > 1) // if arab son exist
            dialIt(dialVar,dialVarArabSon); 
            else
            dialIt(dialVar,dialVar);

            StaticScript.FinishIt(condName);
        }

    }

    void dialIt(DialogueVar[] dial,DialogueVar[] dialArabSon) // dialogue will be activated
    {
        theDialogue.SetDialogue(dialVar,dialVarArabSon);
        theDialogue.StartDialogue();
        beginDialog = false;

    }


    bool ScriptCondition(string name)
        {

            return !StaticScript.GetCond(name);

        }

}
