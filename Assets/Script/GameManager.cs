using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Singleton
    public static GameManager sharedInstance;
    public float velocidadObstaculos, velocidadPlayer,velocidadEstrellas;
    // UI vars
    private Animator _pauseAnimator;
    [SerializeField]
    //private GameObject _gameOverPanel;

    public Text currentScoreText;//, gameScoreText, maxScoreText;

    // General vars
    public int gameScore;
    public int maxScore;
    private int controls;
    public int s;
    private AudioSource _gameScoreAudio, _maxScoreAudio;

    GameObject Player;

    void Awake() 
    {
        _gameScoreAudio = currentScoreText.GetComponent<AudioSource>();
        //_maxScoreAudio = maxScoreText.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player= GameObject.Find("Player");

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
        //gameScoreText.text = "0";
        //maxScoreText.text = string.Format("{0}", maxScore);
        iniciarTimer();
    }

    // Update is called once per frame
    void Update()
    {
        velocidadjuego();
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
        GameObject.Find("NewRecord").GetComponent<Text>().enabled = false;
        //_gameOverPanel.SetActive(false);
        gameScore = 0;
        //gameScoreText.text = string.Format("{0}", 0);
        currentScoreText.text = string.Format("{0}", 0);

        // TODO: reset game elements
    }

    // Increase the current score by "value"
    public void IncreaseScore(int value)
    {
        gameScore += value;
        currentScoreText.text = string.Format("{0}", gameScore);
        //gameScoreText.text = string.Format("{0}", gameScore);
        //_gameScoreAudio.Play(0);
    }

    // Set max score 
    public void UpdateMaxScore()
    {
        if (gameScore > maxScore) 
        {
            GameObject.Find("NewRecord").GetComponent<Text>().enabled = false;
            maxScore = gameScore;
            PlayerPrefs.SetInt("maxScore", gameScore);
            //maxScoreText.text = string.Format("{0}", gameScore);
            //_maxScoreAudio.Play(0);
        }
    }

    // Reset max score
    public void ResetMaxScore()
    {
        PlayerPrefs.SetInt("maxScore", 0);
    }

    //Funcion para aumentar la velocidad del juego
    public void velocidadjuego(){


        float velocidadInicial= 4;

        velocidadObstaculos=(float)velocidadInicial+(s/5);
        velocidadPlayer=(float)10+(s/5);
        velocidadEstrellas=(float)1+(s/5);

        if (Player.GetComponent<PlayerController>().vida<=0){
            CancelInvoke();
            s=0;
            velocidadObstaculos=0.5f;
            velocidadPlayer=1f;
            velocidadEstrellas=0.1f;
            Player.GetComponent<Collider2D>().enabled=false;

        }
    }

    public void iniciarTimer(){
        Invoke("actualizarTimer",1f);
    }

    public void detenerTimer(){
        CancelInvoke();
    }

    public void actualizarTimer(){
        s++;
        Invoke("actualizarTimer",1f);

    }

}