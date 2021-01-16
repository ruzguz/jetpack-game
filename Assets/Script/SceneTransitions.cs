using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public string sceneName;
    private Animator transitionAnimator;

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

}
