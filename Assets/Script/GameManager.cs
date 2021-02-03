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
    public int controls;

    void Awake() 
    {
        
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
        Debug.Log(controls);
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

}
