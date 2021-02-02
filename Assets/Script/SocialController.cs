using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialController : MonoBehaviour
{

    private string INSTAGRAM_URL = "https://www.instagram.com";


    public void OpenIG(string user)
    {
        string url = string.Format("{0}/{1}", INSTAGRAM_URL, user);
        Debug.Log(url);
        Application.OpenURL(url);
    }

}
