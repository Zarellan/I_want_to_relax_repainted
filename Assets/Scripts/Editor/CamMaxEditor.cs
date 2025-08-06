using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class CamMaxEditor : MonoBehaviour
{
    


    [Header("camMaxPos")]
    [HideInInspector]
    public float left,right,up,down;
    [CustomEditor(typeof(CamMaxEditor))]

    #region Editor
    #if UNITY_EDITOR
    public class CamMaxEdit:Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            

            CamMaxEditor camMax = (CamMaxEditor)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("maxCamPos");
            EditorGUILayout.BeginHorizontal();

            int maxWidth = 30;

            EditorGUILayout.LabelField("left",GUILayout.MaxWidth(maxWidth - 5));
            camMax.left = EditorGUILayout.FloatField(camMax.left,GUILayout.MaxWidth(maxWidth));
            
            EditorGUILayout.LabelField("right",GUILayout.MaxWidth(maxWidth));
            camMax.right = EditorGUILayout.FloatField(camMax.right,GUILayout.MaxWidth(maxWidth));

            EditorGUILayout.LabelField("up",GUILayout.MaxWidth(maxWidth - 13));
            camMax.up = EditorGUILayout.FloatField(camMax.up,GUILayout.MaxWidth(maxWidth));

            EditorGUILayout.LabelField("down",GUILayout.MaxWidth(maxWidth + 8));
            camMax.down = EditorGUILayout.FloatField(camMax.down,GUILayout.MaxWidth(maxWidth));

            EditorGUILayout.EndHorizontal();


                // Save changes to the prefab instance
                if (GUI.changed)
                {
                    //PrefabUtility.RecordPrefabInstancePropertyModifications(target);
                    
                }


        }
        
    }
    #endif
    #endregion


}