using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ballHandler : MonoBehaviour
{
    #region variables
    Rigidbody ball;
    UImanager uiManager;
    SoundManager soundManager;

    public bool smash;
    public bool win;        //for one level update once collided
    public bool invinsible;

    public float downSpeed;
    public float bounceSpeed;
    public float pillarRotSpeed;
    public float deathTime=.1f;
    private float deathTimeCounter = 0f;
    public float invinsibilityDuration=1f;
    public float invinsibilityDurationCounter;

    public int currentScore;

    public GameObject splashObject;
    public GameObject splashSprite;
    public GameObject invinsibilePanel;
    public Image invinsibilityIndicator;
    #endregion
    private void Awake()
    {
        smash = false;
        ball = GetComponent<Rigidbody>();
        uiManager = GameObject.FindObjectOfType<UImanager>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        stopBall(0);
        win = false;

        invinsibilityDurationCounter = 0f;
        deathTimeCounter = deathTime;
        invinsible = false;
        invinsibilePanel.SetActive(false);
    }
    public void stopBall(int i)
    {
        if (i == 0)
            ball.isKinematic = true;
        else
            ball.isKinematic = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="Win")
        {
            if (!win)
            {
                soundManager.WinSound();
                uiManager.nextLevelPanel();
                uiManager.updateScore(true);
               // GameObject.FindObjectOfType<LevelManager>().nextLevel();          
                
                win = true;
            }
        }
        if(!smash)
        {
            GameObject g = Instantiate(splashObject, transform.position, splashObject.transform.rotation);
            //GameObject gs = Instantiate(splashSprite, transform.position+Vector3.down*.15f, splashSprite.transform.rotation);
            //gs.transform.parent = collision.collider.transform;
            //Destroy(gs, 9f);
            Destroy(g, 2f);
            soundManager.BounceSound();
            ball.velocity = new Vector3(0, bounceSpeed, 0);         //bouncing back
        }
        else
        {
            if (collision.collider.tag == "enemy"||invinsible)
            {
                soundManager.NormalBreakSound();
                Destroy(collision.collider.transform.parent.gameObject);
                currentScore++;
                uiManager.updateScore();
            }
            else if(collision.collider.tag=="plane")
            {
                //when about to die but survived.
                collision.collider.transform.parent.transform.DOShakeScale(.05f, .2f, 2, 45f, true);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(!smash)
        {
            
            ball.velocity = new Vector3(0, bounceSpeed, 0);         //bouncing back
        }
        else
        {
            if (collision.collider.tag == "enemy"|| invinsible)
            {
                Destroy(collision.collider.transform.parent.gameObject);
            }
            else if (collision.collider.tag == "plane")
            {
                deathTimeCounter -= Time.deltaTime;
                if (deathTimeCounter < 0f)
                {
                    restartFun();
                }
            }
        }
    }
    private void restartFun()
    {
        smash = false;
        soundManager.DeadSound();
        //GameObject.FindObjectOfType<LevelManager>().Restart();
        uiManager.restartPanel();
        deathTimeCounter = deathTime;

        invinsibilityDurationCounter = 0f;
        invinsible = false;
    }
    private void OnCollisionExit(Collision collision)
    { 
        if (collision.collider.tag.Equals("plane"))
        {
            if (deathTimeCounter != deathTime)
                deathTimeCounter = deathTime;
        }
    }
    public void destroy(GameObject g)
    {
        for(int i=0;i<g.transform.childCount;i++)
        {
            //g.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            g.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.up * 200f);
        }
        Destroy(g, 2f);
    }
    private void FixedUpdate()
    {
        if(smash)
        {
            ball.velocity = new Vector3(0, downSpeed, 0);
        }
       

        if (ball.velocity.y > bounceSpeed)
             ball.velocity = new Vector3(ball.velocity.x, bounceSpeed, ball.velocity.z);
        
    }
    private void Update()
    {
        if(smash && invinsibilityDurationCounter<invinsibilityDuration)
        {
            if (!invinsibilePanel.gameObject.activeInHierarchy)
                invinsibilePanel.SetActive(true);
            invinsibilityDurationCounter += Time.deltaTime;

            invinsibilityIndicator.DOFillAmount(invinsibilityDurationCounter / invinsibilityDuration, .1f);
        }   
        if(!smash)
        {
            if (invinsibilityDurationCounter >= 0f)
            {
                invinsibilityDurationCounter -= Time.deltaTime;
                invinsibilityIndicator.DOFillAmount(invinsibilityDurationCounter / invinsibilityDuration, .1f);
            }
            if(invinsibilityDurationCounter<=0f)
            {
                invinsibilePanel.SetActive(false);
                invinsible = false;
            }
        }
        if(invinsibilityDurationCounter>=invinsibilityDuration)
        {
            invinsible = true;
        }

    }
}
