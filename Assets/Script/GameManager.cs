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
    private GameObject _gameOverPanel;
    
    public Text currentScoreText, gameScoreText, maxScoreText, _newRecordLabel;

    // General vars
    public int gameScore;
    public int maxScore;
    private int controls;
    private AudioSource _gameScoreAudio, _maxScoreAudio;

    void Awake() 
    {
        _gameScoreAudio = currentScoreText.GetComponent<AudioSource>();
        _maxScoreAudio = maxScoreText.GetComponent<AudioSource>();
        _gameOverPanel = GameObject.Find("GameOverPanel");
        _newRecordLabel = GameObject.Find("NewRecord").GetComponent<Text>();
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

        // TODO: Pause game elements
    }

    // Hide puse menu and resume the game
    public void ResumeGame() 
    {
        _pauseAnimator.SetBool("pauseGame", false);
        Debug.Log("RESUME");

        // TODO: Resume game elements
    }

        // Funtion to start a new game
    public void ResetGame()
    {
        // Reset UI Elements
        _gameOverPanel.SetActive(false);
        _newRecordLabel.enabled = false;
        gameScore = 0;
        gameScoreText.text = string.Format("{0}", 0);
        currentScoreText.text = string.Format("{0}", 0);

        // TODO: reset game elements
        
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
            _newRecordLabel.enabled = true;
            maxScore = gameScore;
            PlayerPrefs.SetInt("maxScore", gameScore);
            maxScoreText.text = string.Format("{0}", gameScore);
            _maxScoreAudio.Play(0);
        }
    }

    // Reset max score
    public void ResetMaxScore()
    {
        PlayerPrefs.SetInt("maxScore", 0);
    }


}