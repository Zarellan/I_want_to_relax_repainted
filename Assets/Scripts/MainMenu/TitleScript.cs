using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{

    public Animation anim;

    public Animator animar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartAnim()
    {
        //anim.Play("repaintedText");

        animar.SetBool("StartAnim",true);
    }
}
