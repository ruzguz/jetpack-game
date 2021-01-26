using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    private Animator _panelAnimator;
    
    private void Awake() {
        _panelAnimator = GetComponent<Animator>();
    }

    public void MoveIn()
    {
        _panelAnimator.SetBool("pauseGame", true);
    }

    public void MoveOut()
    {
        _panelAnimator.SetBool("pauseGame", false);
    }
    
}
