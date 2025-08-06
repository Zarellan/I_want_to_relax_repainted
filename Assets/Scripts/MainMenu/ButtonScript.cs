using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonScript : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    public Image spriteRend;
    public Text text;


    public float posY;

    public float differenceY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x,(Mathf.Cos(Time.time + differenceY) / 4f) + posY);

    }
    


    public void OnPointerEnter(PointerEventData eventData)
    {
        Tween.Scale(transform,endValue:1.3f,duration:0.3f,Ease.OutSine);
        Tween.Color(text,endValue:Color.yellow,duration:0.3f,Ease.OutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.Scale(transform,endValue:1f,duration:0.3f,Ease.OutSine);
        Tween.Color(text,endValue:Color.white,duration:0.3f,Ease.OutSine);
    }

}
