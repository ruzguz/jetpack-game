using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialController : MonoBehaviour
{

    private string RUZGUZ_IG = "https://www.instagram.com/_ruzguz/";

    public void OpenRuzguzIG()
    {
        Application.OpenURL(RUZGUZ_IG);
    }

}
