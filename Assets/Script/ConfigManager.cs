using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{


    // Vars
    private int _controls;


    // UI vars
    public Button reverseControlsBtn;
    public Sprite[] btnSprites; // 0 = UIbtn, 1 = UIBtn-pressed, 2 = UIBtnON, 3 = UIBtnOn-pressed


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("controls") == 0) 
        {
            PlayerPrefs.SetInt("controls", -1);
            PlayerPrefs.Save();
        }

        _controls = PlayerPrefs.GetInt("controls");
        //Debug.Log(_controls);

        SetUI();
    }

    // Function to set UI Accourding to settings values 
    public void SetUI()
    {
        // Setup reverse controls button
        Text reverseControlsBtnText = reverseControlsBtn.GetComponentInChildren<Text>();
        Image reverseControlsBtnImage = reverseControlsBtn.GetComponent<Image>();
        if (_controls == -1)
        {
            reverseControlsBtnText.text = "OFF";
            reverseControlsBtnImage.sprite = btnSprites[0];
        } else {
            reverseControlsBtnText.text = "ON";
            reverseControlsBtnImage.sprite = btnSprites[2];
        }
    }

    public void SetPressedButton()
    {
        reverseControlsBtn.GetComponentInChildren<Text>().text = "";    
        Image reverseControlsBtnImage = reverseControlsBtn.GetComponent<Image>();
        if (_controls == -1)
        {
            reverseControlsBtnImage.sprite = btnSprites[1];
        } else 
        {
            reverseControlsBtnImage.sprite = btnSprites[3];
        }

    }

    public void ReverseControls()
    {
        _controls*=-1;
        PlayerPrefs.SetInt("controls", _controls);
        Debug.Log(_controls);
        SetUI();
    }

}
