using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    ballHandler ball;
    private void Awake()
    {
        ball = GameObject.FindObjectOfType<ballHandler>();
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        ball.smash = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        ball.smash = true;
    }
}
