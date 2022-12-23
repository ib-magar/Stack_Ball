using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class cameraManager : MonoBehaviour
{
    ballHandler ball;
    public float speed;
    public float distanceOffset;

    public Transform winObj;
    public Transform centerCylinder;
    
    private void Awake()
    {
        ball = GameObject.FindObjectOfType<ballHandler>();
    }
    private void Start()
    {
        resetPosition();
    }

    private void Update()
    {
        if (winObj == null)
            winObj = GameObject.FindGameObjectWithTag("Win").transform;
        else
            winObj = null;

        if (winObj != null)
        {
            if (Mathf.Abs(transform.position.y - ball.transform.position.y) > distanceOffset * 2f && Mathf.Abs(winObj.position.y - transform.position.y) > distanceOffset * 3f)
            {
                transform.DOMoveY(ball.transform.position.y + distanceOffset, speed);
            }
        }
        if(Mathf.Abs(transform.position.y-centerCylinder.transform.position.y)>distanceOffset*2f /*&& centerCylinder.position.y>winObj.transform.position.y+distanceOffset*15f*/)
        {
            centerCylinder.transform.DOMoveY(transform.position.y+distanceOffset*2f,.1f);
        }
    }
   public void resetPosition()
    {
        transform.DOMoveY(ball.transform.position.y + distanceOffset, speed);
        centerCylinder.transform.DOMoveY(transform.position.y, .1f);
    }
}
