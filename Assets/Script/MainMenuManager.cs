using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public AudioSource transitionAudio;

    void Awake()
    {
        transitionAudio = GameObject.Find("TransitionAudio").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTransitionAudio()
    {
        transitionAudio.Play();
    }
}
