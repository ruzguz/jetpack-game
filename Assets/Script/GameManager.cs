using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Singleton
    public static GameManager sharedInstance;
    
    // UI vars
    private Animator _pauseAnimator; 
    
    public Text currentScoreText, gameScoreText, maxScoreText;

    // General vars
    public int gameScore;
    public int maxScore;
    private int controls;
    private AudioSource _gameScoreAudio, _maxScoreAudio;

    void Awake() 
    {
        _gameScoreAudio = currentScoreText.GetComponent<AudioSource>();
        _maxScoreAudio = maxScoreText.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Singleton validation
        if (sharedInstance == null) 
        {
            sharedInstance = this;
        } else 
        {
            Destroy(this);
        }

        _pauseAnimator = GameObject.Find("PausePanel").GetComponent<Animator>();

        // Get Controls value
        controls = PlayerPrefs.GetInt("controls");
        

        // Initilizate scores
        gameScore = 0;
        maxScore = PlayerPrefs.GetInt("maxScore");

        currentScoreText.text = "0";
        gameScoreText.text = "0";
        maxScoreText.text = string.Format("{0}", maxScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pause game and show pause menu
    public void PauseGame()
    {
        _pauseAnimator.SetBool("pauseGame", true);
        Debug.Log("PAUSE");
    }

    // Hide puse menu and resume the game
    public void ResumeGame() 
    {
        _pauseAnimator.SetBool("pauseGame", false);
        Debug.Log("RESUME");
    }

    // Ends game and go to main menu 
    public void ExitGame()
    {
        Debug.Log("EXIT GAME");
    }


        // Increase the current score by "value"
    public void IncreaseScore(int value)
    {
        gameScore += value;
        currentScoreText.text = string.Format("{0}", gameScore);
        gameScoreText.text = string.Format("{0}", gameScore);
        _gameScoreAudio.Play(0);
    }

    // Set max score 
    public void UpdateMaxScore()
    {
        if (gameScore > maxScore) 
        {
            PlayerPrefs.SetInt("maxScore", gameScore);
            maxScoreText.text = string.Format("{0}", gameScore);
            _maxScoreAudio.Play(0);
        }
    }

    public void ResetMaxScore()
    {
        PlayerPrefs.SetInt("maxScore", 0);
    }

    public void ResetGame()
    {
        GameObject.Find("GameOverPanel").SetActive(false);
    }

}