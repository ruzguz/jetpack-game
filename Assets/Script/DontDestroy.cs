using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy sharedInstance;

    void Awake() 
    {
        if (sharedInstance != null && sharedInstance != this) 
        {
            Destroy(this.gameObject);
        } else 
        {
            sharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
