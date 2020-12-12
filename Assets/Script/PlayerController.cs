using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D fisica;
    public int fuerza,desplazamiento,rotacion;
    public GameObject imagen;
    public Animator Personaje, CoheteDerecho, CoheteIzquierdo;
    static int MaxRotIzquierda = 38, MaxRotaDerecha = -MaxRotIzquierda; //la maxima rotacion que tiene el Jetpack

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        Personaje = gameObject.transform.GetChild(0).GetComponent<Animator>();
        CoheteDerecho = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>();
        CoheteIzquierdo = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        //cuando preciona alguna palanca hace fuerza hacia arriba
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            fisica.AddRelativeForce(transform.up * fuerza * Time.deltaTime, ForceMode2D.Impulse);
            CoheteDerecho.SetTrigger("isFlying");
            CoheteIzquierdo.SetTrigger("isFlying");

        }

        if (Input.GetKey("a"))
        {
            //cuanda preciona a se desplaza a la derecha
            fisica.AddRelativeForce(transform.right * desplazamiento * Time.deltaTime, ForceMode2D.Impulse);
            if (imagen.transform.localRotation.z * 100 > MaxRotaDerecha)
                //rota en sentido del reloj
                imagen.transform.Rotate(new Vector3(0, 0, -rotacion * Time.deltaTime));
        }
        if (Input.GetKey("d"))
        {
            //cuanda preciona a se desplaza a la izquierda

            fisica.AddRelativeForce(-transform.right * desplazamiento * Time.deltaTime, ForceMode2D.Impulse);
            if (imagen.transform.localRotation.z * 100 < MaxRotIzquierda)
                //rota en sentido del contrario al reloj
                imagen.transform.Rotate(new Vector3(0, 0, rotacion * Time.deltaTime));
        }


        //automaticamente debe ir corrigiendo su rotacion para que se endereze solo
        if ((int)(imagen.transform.localRotation.z * 100) < 0 )
            imagen.transform.Rotate(new Vector3(0, 0, rotacion/3 * Time.deltaTime));

        if ((int)(imagen.transform.localRotation.z * 100) >= 0)
            imagen.transform.Rotate(new Vector3(0, 0, -rotacion/3 * Time.deltaTime));

    }




}
