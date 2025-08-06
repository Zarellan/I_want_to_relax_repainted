using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
[System.Serializable]
public static class StaticScript
{
    public static bool[] conditionDialogue;
    public static string[] conditionDialogueName;

    public static bool added = false;
    static int index = 0;

    static bool changed = false;

    static StaticScript()
    {
        if (!changed)
        {
        conditionDialogue = new bool[20];
        conditionDialogueName = new string[20];

        AddCond("zarellan in toilet");
        AddCond("hamboza with father");
        AddCond("not changed clothes");
        AddCond("fighted bullies");
        AddCond("asked for out");
        AddCond("tor Mention");
        AddCond("school1");

        changed = true;
        }

        
    }

    

    static void AddCond(string name)
    {
        conditionDialogueName[index] = name;
        conditionDialogue[index] = false;

        index += 1;

    }


    public static bool GetCond(string name)
    {
        for (int i = 0;i < conditionDialogueName.Length;i++)
        {

            if (conditionDialogueName[i] == name)
            {
                return conditionDialogue[i];
            }

        }

        return false;
    }


    public static void FinishIt(string name)
    {
        for (int i = 0;i <conditionDialogueName.Length;i++)
                {

                    if (conditionDialogueName[i] == name)
                    {
                        conditionDialogue[i] = true;

                    }

                }
    }
}
