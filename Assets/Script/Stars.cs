﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public float velocidad;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*velocidad*Time.deltaTime);
        if (transform.position.y <= -6.6f)
        {
            transform.position = new Vector3(transform.position.x, 5.25f, 1);
        }
    }
}
