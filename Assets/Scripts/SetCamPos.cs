using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamPos : MonoBehaviour
{
    [SerializeField]
    float x,y,size;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("MainCamera").transform.position = new Vector3(x,y,GameObject.FindWithTag("MainCamera").transform.position.z);
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = size; 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
