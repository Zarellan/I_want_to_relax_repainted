using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FlySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject fly;

    
    [SerializeField]
    VideoClip[] videos;


    // Start is called before the first frame update
    void Start()
    {
        SpawnBird();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBird()
    {
        GameObject flyObject = Instantiate(fly,new Vector2(transform.position.x,transform.position.y-2),Quaternion.identity,this.transform);
        flyObject.GetComponent<VideoPlayer>().clip = videos[Random.Range(0,videos.Length)];
        flyObject.GetComponent<VideoPlayer>().Play();
        StartCoroutine(Timer.timer(3,SpawnBird));
    }
}
