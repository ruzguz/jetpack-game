using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public GameObject GameManager;

    public float velocidad;
    // Update is called once per frame

    void Start(){
        GameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        velocidad=GameManager.GetComponent<GameManager>().velocidadEstrellas;
        transform.Translate(Vector3.down*velocidad*Time.deltaTime);
        if (transform.position.y <= -6.6f)
        {
            transform.position = new Vector3(transform.position.x, 5.25f, 1);
        }
    }
}
