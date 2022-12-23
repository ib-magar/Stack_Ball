using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UImanager : MonoBehaviour
{
    ballHandler ball;
    LevelManager levelManager;
    SoundManager soundManager;

    public Image progressBar;

    public GameObject Menu;
    public GameObject nextLevelMenu;
    public GameObject restartMenu;
    public GameObject splashObject;
    private void Start()
    {
        ball = GameObject.FindObjectOfType<ballHandler>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        updateScore();

        nextLevelMenu.SetActive(false);
        restartMenu.SetActive(false);
        Menu.SetActive(true);
    }
    public void updateScore(bool win = false)
    {
        if (!win)
            progressBar.DOFillAmount(ball.currentScore / (float)(levelManager.currentLevelData.totalStacks + 1), .1f);
        else
            progressBar.DOFillAmount(1, .1f);
    }
    public void play()
    {
        soundManager.ButtonClickSound();
        soundManager.fade(1);
        Menu.SetActive(false);
    }
    public void menu()
    {
        restartMenu.SetActive(false);
        nextLevelMenu.SetActive(false);
        Menu.SetActive(true);
    }
    public void nextLevelPanel()
    {
        nextLevelMenu.SetActive(true);  
    }
    public void nextLevelLoad()
    {
        soundManager.ButtonClickSound();
        nextLevelMenu.SetActive(false);
        levelManager.nextLevel();
    }
    public void restartPanel()
    {
        restartMenu.SetActive(true);
        progressBar.DOFillAmount(0, .1f);
        
    }
    public void restartLoad()
    {
        soundManager.ButtonClickSound();
        restartMenu.SetActive(false);
        levelManager.createLevel();
    }
    public void quitGame()
    {
        Application.Quit();
    }
    
}
