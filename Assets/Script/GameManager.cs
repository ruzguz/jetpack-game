﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Singleton
    public static GameManager sharedInstance;
    
    // UI vars
    private 

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pause game and show pause menu
    public void PauseGame()
    {
        Debug.Log("PAUSE");
    }

    // Hide puse menu and resume the game
    public void ResumeGame() 
    {
        Debug.Log("RESUME");
    }

    // Ends game and go to main menu 
    public void ExitGame()
    {
        Debug.Log("EXIT GAME");
    }

}