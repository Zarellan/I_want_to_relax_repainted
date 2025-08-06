using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Android;
using Unity.Mathematics;






#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.EditorTools;
#endif

public class PlayerScript : MonoBehaviour
{

    Animator anim;

    Animation anim2;

    Vector2 positiono;

    float xx;
    float yy;

    string currentAnimation;

    public static float posStartX;
    public static float posStartY;


    [SerializeField]
    float playerSpeed;

    GameObject cam;

    public bool canMove = true;


    public bool keepFollowCam = false;


    public GameObject dialo;

    public GameObject flashLight;

    public GameObject musicPlayer;

    float camSpeedX = 0;
    float camSpeedY = 0;


    public SpriteRenderer backGround;


    private float[] maxCam;

    [SerializeField]
    bool posDebug = false;


    string direction = "down"; //yes I want it string because why not


    [SerializeField]
    float CamMaxLeft;
    [SerializeField]
    float CamMaxRight;
    [SerializeField]
    float CamMaxUp;
    [SerializeField]
    float CamMaxDown;


    // Start is called before the first frame update
    void Start()
    {
        maxCam = new float[4];

        positiono = new Vector2();

        if (!posDebug)
        transform.position = new Vector2(posStartX,posStartY);

        
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        anim2 = GameObject.FindWithTag("Player").GetComponent<Animation>();

        cam = GameObject.FindWithTag("MainCamera");

        if (GameObject.FindWithTag("Dialogue") == null)
            Instantiate(dialo); 
        //dialo.GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        if (GameObject.FindWithTag("FlashLight") == null)
        {
        Instantiate(flashLight);
        flashLight.GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        if (GameObject.FindWithTag("MusicPlayer") == null)
        {
            Instantiate(musicPlayer);
        //flashLight.GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        
        GameObject camObj = GameObject.FindWithTag("CamPosMaxObj");
        maxCam[0] = CamMaxLeft;
        maxCam[1] = CamMaxRight;
        maxCam[2] = CamMaxUp;
        maxCam[3] = CamMaxDown;



        
        //cam.transform.position = new Vector3(posStartXCam,posStartYCam,-10);

    }

    void FixedUpdate()
    {
        /*Vector2 hello = transform.position;

        xx = Input.GetAxisRaw("Horizontal");
        yy = Input.GetAxisRaw("Vertical");


        position.x += xx * playerSpeed * Time.deltaTime;

        position.y += yy * playerSpeed * Time.deltaTime;

        transform.position = hello;*/
        if (canMove)
            {
            positiono = transform.position;
            //Vector2 
            xx = Input.GetAxisRaw("Horizontal");
            yy = Input.GetAxisRaw("Vertical");

            positiono.x += xx * playerSpeed * Time.deltaTime;

            positiono.y += yy * playerSpeed * Time.deltaTime;

        

        
            transform.position = positiono;
            }

            float xDiff = transform.position.x - cam.transform.position.x;
            float yDiff = transform.position.y - cam.transform.position.y;

            camSpeedX = camSpeedX / 2 + xDiff / 20;
            camSpeedY = camSpeedY / 2 + yDiff / 20;
            if (keepFollowCam)
            {
                cam.transform.position = new Vector3(Mathf.Clamp(transform.position.x,maxCam[0],maxCam[1]),Mathf.Clamp(transform.position.y,maxCam[3],maxCam[2]),-10);
            }



    }


    // Update is called once per frame
    void Update()
    {
        //dialo.GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        if (canMove)
        {

        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ChangeAnimation("Up");
            direction = "up";

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ChangeAnimation("down");
            direction = "down";

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ChangeAnimation("Left");
            direction = "left";
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ChangeAnimation("Right");
            direction = "right";
        }
        else
        {
            Idle();
        }
        //anim.SetInteger("PosY",Mathf.FloorToInt(yy));
        //anim.SetInteger("PosX",Mathf.FloorToInt(xx));
        }


    }

    public void Idle()
    {

            switch(direction)
            {
                case "down":ChangeAnimation("idle");break;
                case "right":ChangeAnimation("Idle right");break;
                case "left":ChangeAnimation("Idle left");break;
                case "up":ChangeAnimation("Idle up");break;

            }
    }


    void ChangeAnimation(string newAnimation)
    {

        if (currentAnimation == newAnimation)
            return;

        anim.Play(newAnimation);

    }
}

