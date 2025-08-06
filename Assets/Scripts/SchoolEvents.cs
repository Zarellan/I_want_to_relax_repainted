using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;

public class SchoolEvents : MonoBehaviour
{

    [SerializeField]
    TMP_Text cannotBeatUs;

    [SerializeField]
    GameObject particleStar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RevealText()
    {
        Tween.Alpha(cannotBeatUs,1,3,Ease.Linear);
        Tween.PositionX(particleStar.transform,2.52f,3,Ease.Linear);
        particleStar.GetComponent<ParticleSystem>().Play();
    }


    public void ChangeText(string text)
    {
        cannotBeatUs.text = text;
    }
}
