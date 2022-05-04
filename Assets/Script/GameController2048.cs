using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController2048 : MonoBehaviour
{
    public static GameController2048 instance;
    public static int ticker;

    [SerializeField] GameObject fillPrefab;
    [SerializeField] Cell2048[] allCells;
    public static Action<string> slide;
    public int myScore;
    [SerializeField] Text scoreDisplay;
    int isGameOver;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    public Color[] fillColors;
    [SerializeField] int winningScore;
    [SerializeField] GameObject winningPanel;
    bool hasWon;
    private AudioSource audioSourceBackGround;
    private AudioSource audioGame;

    private Vector2 startPos;
    public int pixelDistToDetect = 50;
    private bool fingerDown;
    private void OnEnable() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start() 
    {
        AdManager.instance.RequestBanner();
        audioSourceBackGround = gameObject.GetComponent<AudioSource>(); 
        StartSpawnFill();
        StartSpawnFill();
    }
    void Update()
    {
        //Keycode for pc
        if(Input.GetKeyDown(KeyCode.Space))
        {
        SpawnFill();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("up");

        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("down");

        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("left");

        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("right");
        }


        if(fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }
        if(fingerDown)
        {
            if(Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe up");
                ticker = 0;
                isGameOver = 0;
                slide("up");
            }
            else if(Input.touches[0].position.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe down");
                ticker = 0;
                isGameOver = 0;
                slide("down");                
            }
            else if(Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe left");
                ticker = 0;
                isGameOver = 0;
                slide("left");
            }
            else if(Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe right");
                ticker = 0;
                isGameOver = 0;
                slide("right");
            }
        }
        if(fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
        }

        //Swipe for pc
        if(fingerDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }
        if(fingerDown)
        {
            if(Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                ticker = 0;
                isGameOver = 0;
                slide("up");
            }
            else if(Input.mousePosition.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                ticker = 0;
                isGameOver = 0;
                slide("down");
            }
            else if(Input.mousePosition.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                ticker = 0;
                isGameOver = 0;
                slide("left");
            }
            else if(Input.mousePosition.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                ticker = 0;
                isGameOver = 0;
                slide("right");
            }
        }
        if(fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }

    }
    public void SpawnFill()
    {
        bool isFull = true;
        for(int i = 0; i < allCells.Length; i++ )
        {
            if(allCells[i].fill == null)
            {
                isFull = false;
            }
        }
        if(isFull == true)
        {
            return;
        }

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if(allCells[whichSpawn].transform.childCount != 0)
        {
            //Debug.Log(allCells[whichSpawn].name + " is already filled");
            SpawnFill();
            return;
        }
        float chance = UnityEngine.Random.Range(0, 1f);
        //Debug.Log(chance);

        if(chance < 0.2f)
        {
            return;
        }
        if(chance < 0.85f)
        {
            //int whichSpawn = Random.Range(0, allCells.Length);
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            //Debug.Log("2");
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else
        {
            //int whichSpawn = Random.Range(0, allCells.Length);
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            //Debug.Log("4");
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }

    public void StartSpawnFill()
    {
        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length); //Ramdom.Range co o ca namespace System and UnityEngine
        if(allCells[whichSpawn].transform.childCount != 0)
        {
           //Debug.Log(allCells[whichSpawn].name + " is already filled");
            SpawnFill();
            return;
        }
        
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
           //Debug.Log("2");
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
    }
    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        scoreDisplay.text = myScore.ToString();
    }
    public void GameOverCheck()
    {
        isGameOver++;
        if(isGameOver >= 16)
        {
            gameOverPanel.SetActive(true);
            audioGame = gameOverPanel.GetComponent<AudioSource>();
            audioSourceBackGround.mute = true;
            //audioGame.clip = gameOverClip;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void WinningCheck(int highestFill)
    {
        if(hasWon)
        return;

        if(highestFill == winningScore)
        {
            winningPanel.SetActive(true);
            hasWon = true;
            audioGame = winningPanel.GetComponent<AudioSource>();
        }
    }
    public void KeepPlaying()
    {
        winningPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void CloseButton()
    {
        pausePanel.SetActive(true);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
