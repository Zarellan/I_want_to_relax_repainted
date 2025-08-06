using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingThings : MonoBehaviour
{
    float defaultPosx;
    float defaultPosY;
    float randomPosY;



    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.position.y;
        randomPosY = Random.Range(0.0f,5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * 2,defaultPosY + Mathf.Sin(Time.time*3 + randomPosY) / 2);
        
        if (transform.position.x > 37)
        {
            Destroy(gameObject);
        }
    }
}
