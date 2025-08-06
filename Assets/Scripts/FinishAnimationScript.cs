using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAnimationScript : MonoBehaviour // for Icon
{
    

    void FinishIt()
    {
    GameObject.FindWithTag("Dialogue").GetComponent<Dialogue>().FinishIconAnimation();

    }


}
