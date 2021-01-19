using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{

    // General vars
    public bool makeTransition;
    public string sceneName;
    private Animator transitionAnimator;

    // Delegate to run method when the scene is loaded 
    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {

        if (makeTransition) 
        {
            Debug.Log("Scene Loaded");
            runMoveAnimation();
        }

    }
    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake() 
    {
        transitionAnimator = GetComponent<Animator>();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void runTransitionAnimation()
    {
        transitionAnimator.SetTrigger("makeTransition");
    }

    public void runMoveAnimation() 
    {
        transitionAnimator.SetTrigger("move");
    }

}
