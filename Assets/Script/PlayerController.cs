using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D fisica;
    public int fuerza, desplazamiento, rotacion;
    public GameObject imagen;
    public Animator Personaje, CoheteDerecho, CoheteIzquierdo;
    static int MaxRotIzquierda = 38, MaxRotaDerecha = -MaxRotIzquierda; //la maxima rotacion que tiene el Jetpack

    public int posMin;
    public float tiempoParticula;
    public GameObject ParticulaIzq, ParticulaDer;
    public int MaxPosicion;


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
        //cuando preciona alguna palanca hace fuerza hacia arriba, Impulso vertical
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            if (transform.position.y < MaxPosicion)
                fisica.AddRelativeForce(transform.up * fuerza * Time.deltaTime, ForceMode2D.Force);
                //gameObject.transform.Translate (Vector2.up * Time.deltaTime*fuerza);
        }

        //Movimiento del Personaje
        if (Input.GetKey("a"))
            IniciarCoheteIzquierdo();
        if (Input.GetKey("d"))
            IniciarCoheteDerecho();


        //automaticamente debe ir corrigiendo su rotacion para que se endereze solo 
        if ((int)(imagen.transform.localRotation.z * 100) < 0)
            imagen.transform.Rotate(new Vector3(0, 0, rotacion / 3 * Time.deltaTime));

        if ((int)(imagen.transform.localRotation.z * 100) >= 0)
            imagen.transform.Rotate(new Vector3(0, 0, -rotacion / 3 * Time.deltaTime));

        //si el personaje esta abajo entonces sube activando los cohetes hasta 1/4 de la pantalla 
        if (transform.position.y < posMin)
        {
            //gameObject.transform.Translate (Vector2.up * Time.deltaTime*fuerza);
            fisica.AddRelativeForce(transform.up * fuerza * Time.deltaTime, ForceMode2D.Force);
            IniciarCoheteIzquierdo();IniciarCoheteDerecho();
        }


    }

    void IniciarCoheteIzquierdo()
    {
        CoheteDerecho.SetTrigger("isFlying");
        //cuanda preciona a se desplaza a la derecha
        fisica.AddRelativeForce(transform.right * desplazamiento * Time.deltaTime, ForceMode2D.Force);
        if (imagen.transform.localRotation.z * 100 > MaxRotaDerecha)
            //rota en sentido del reloj
            imagen.transform.Rotate(new Vector3(0, 0, -rotacion * Time.deltaTime));
    }

    void IniciarCoheteDerecho()
    {
        //cuanda preciona a se desplaza a la izquierda
        CoheteIzquierdo.SetTrigger("isFlying");
        fisica.AddRelativeForce(-transform.right* desplazamiento * Time.deltaTime, ForceMode2D.Force);
        if (imagen.transform.localRotation.z* 100 < MaxRotIzquierda)
            //rota en sentido del contrario al reloj
            imagen.transform.Rotate(new Vector3(0, 0, rotacion* Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Limite")
        //Sistema de particulas chulas
        {
            if (collision.gameObject.name == "LimiteIzquierdo")
            {
                ParticulaIzq.SetActive(true);
                StartCoroutine(Particulas(ParticulaIzq));                                  

            }
            if (collision.gameObject.name == "LimiteDerecho")
            {
                ParticulaDer.SetActive(true);
                StartCoroutine(Particulas(ParticulaDer));
            }
            //ParticulaChoque.transform.position =(collision.otherCollider.gameObject.transform.position);
        }
    }


    IEnumerator Particulas(GameObject Particula)                                           //0.25 de espera por la animacion y el resto para el parpadeo
    {
        for (float tiempo = Time.time; Time.time - tiempo <tiempoParticula;)                             //tiempo que tarda
            yield return null; //esto significa no hacer nada
        Particula.SetActive(false);
        yield return null;

    }
}

