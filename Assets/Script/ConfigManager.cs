using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{


    // Vars
    private int _controls;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("controls") == 0) 
        {
            PlayerPrefs.SetInt("controls", 1);
        }

        _controls = PlayerPrefs.GetInt("controls");
        Debug.Log(_controls);
    }

    public void ReverseControls()
    {
        _controls*=-1;
        PlayerPrefs.SetInt("controls", _controls);
        Debug.Log(_controls);
    }

}
