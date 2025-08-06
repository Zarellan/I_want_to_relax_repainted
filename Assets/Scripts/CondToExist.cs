using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondToExist : MonoBehaviour
{

    [SerializeField]
    new string name;
    // Start is called before the first frame update
    void Start()
    {
        if (StaticScript.GetCond(name))
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
