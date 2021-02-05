using System.Collections;
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

        if ((int)(imagen.transform.localRotation.z * 100) < 0)                                                             //automaticamente debe ir corrigiendo su rotacion para que se endereze solo 
            imagen.transform.Rotate(new Vector3(0, 0, rotacion / 3 * Time.deltaTime));

        if ((int)(imagen.transform.localRotation.z * 100) >= 0)
            imagen.transform.Rotate(new Vector3(0, 0, -rotacion / 3 * Time.deltaTime));

        
        if (transform.position.y < posMin)                                                                                  //si el personaje esta abajo de la posicion minima...
        {
            fisica.AddRelativeForce(transform.up * fuerza * Time.deltaTime, ForceMode2D.Force);                             //entonces sube activando los cohetes hasta 1/4 de la pantalla 
            IniciarCoheteIzquierdo(); IniciarCoheteDerecho();
        }
    }

    void IniciarCoheteIzquierdo()                                                                                           //Activa el cohete Izquierdo
    {
        CoheteDerecho.SetTrigger("isFlying");                                                                               //Activa la animacion del cohete
        fisica.AddRelativeForce(transform.right * desplazamiento * Time.deltaTime, ForceMode2D.Force);                      //Agrega fuerza al personaje
        if (imagen.transform.localRotation.z * 100 > MaxRotaDerecha)                                                        //Si la rotacion es menor a su rotacion derecha maxima...
            imagen.transform.Rotate(new Vector3(0, 0, -rotacion * Time.deltaTime));                                         //rota en sentido del reloj
    }

    void IniciarCoheteDerecho()                                                                                             //Activa el cohete Derecho
    {
        CoheteIzquierdo.SetTrigger("isFlying");                                                                             //Activa la animacion del cohete
        fisica.AddRelativeForce(-transform.right* desplazamiento * Time.deltaTime, ForceMode2D.Force);                      //Agrega fuerza al personaje
        if (imagen.transform.localRotation.z* 100 < MaxRotIzquierda)                                                        //Si la rotacion es menor a su rotacion izquierda maxima...
            imagen.transform.Rotate(new Vector3(0, 0, rotacion* Time.deltaTime));                                           //rota en sentido del contrario al reloj
    }

    private void OnCollisionEnter2D(Collision2D collision)                                                                  //Ante una colision
    {
        if (collision.gameObject.tag == "Limite")                                                                           //Si colisiono con un limite...
        {                                                                                                                   //Sistema de particulas chulas
            if (collision.gameObject.name == "LimiteIzquierdo")                                                             //Si colisiono con el limite izquierdo                                         
            {
                ParticulaIzq.SetActive(true);                                                                               //Activar las particulas del lado izquierdo del casco
                StartCoroutine(Particulas(ParticulaIzq));                                                                   //Empezar un hilo para mantener las particulas encendidas por un tiempo                          

            }
            if (collision.gameObject.name == "LimiteDerecho")                                                               //Si colisiona con el limite derecho...
            {
                ParticulaDer.SetActive(true);                                                                               //Activar las particulas del lado derecho del casco
                StartCoroutine(Particulas(ParticulaDer));                                                                   //Empezar un hilo para mantener las particulas encendidas por un tiempo
            }
        }
    }


    IEnumerator Particulas(GameObject Particula)                                                                            //0.25 de espera por la animacion y el resto para el parpadeo
    {
        for (float tiempo = Time.time; Time.time - tiempo <tiempoParticula;)                                                //tiempo que tarda en no hacer nada
            yield return null;                                                                                              //esto significa no hacer nada
        Particula.SetActive(false);                                                                                         
        yield return null;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Roca")                                                                                        //si colisiona con una roca
        {
            
            if(Random.Range(0, 2)==1)                                                                                       //Random para determinar si sale disparado a la izquierda o a la derecha
                fisica.AddRelativeForce((transform.right-transform.up) * 0.3f * fuerza * Time.deltaTime, 
                    ForceMode2D.Impulse);                             
            else
                fisica.AddRelativeForce((-transform.right-transform.up) * 0.3f * fuerza * Time.deltaTime, 
                    ForceMode2D.Impulse);                             

        }
    }
}

