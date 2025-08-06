using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDestroyable : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
