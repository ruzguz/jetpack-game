using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{

    //VelocidadUp es la velocidad que tiene el meteoro cuando sube
    //VelocidadDown es la velocidad que tiene el meteoro cuando baja
    //puntoDeAparicion es el punto de la pantalla que el meteoro aparece
    //VelocidadJugador es la velocidad que va el jugador y acelera los meteoros para dar la sensacion que va mas rapido
    //direccion es para el kovimiento lateral del obstaculo 0 si va hacia la derecha y 1 si va hacia la izquierda

    public int VelocidadDown, VelocidadLateral, puntoDeAparicion, velocidadJugador,Direccion;
    public float velocidadTransparencia, tamanoMaximo, alturaMaxima, alturaMin, velocidadCrecimiento, PuntoD,PuntoI;

    int primerPlano, segundoPlano, puntoRetorno, velocidadUp;

    //La velocidad de transparencia es igual distancia/100
    //distancia es igual a punto de aparicion menos altura maxima
    private void Start()
    {
        primerPlano = 1;
        segundoPlano = 0;
        puntoRetorno = 6;
        velocidadUp = VelocidadDown / 2;

    }

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, puntoDeAparicion, 0); //Mueve el objeto a Z=0 e inicializa para volver a empezar
        transform.localScale = new Vector3(1, 1, 0);
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);

    }

    void Update()
    {
        if (!(PuntoD == 0 && PuntoI == 0))
            MovientoLateral();
        velocidadTransparencia = ((255 / (Mathf.Abs(alturaMaxima - puntoDeAparicion + 2))) / 100);
        if (transform.position.z == segundoPlano) //esta en segundo plano 
        {
            if (transform.position.y < puntoRetorno) //llego al punto alto y se prepara que entre al primer plano
            {
                transform.Translate(Vector3.up * Time.deltaTime * velocidadUp);
                if (GetComponent<SpriteRenderer>().color.a < 0.8)
                    GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, velocidadTransparencia * Time.deltaTime * velocidadUp);
            }

            if (transform.localScale.x < tamanoMaximo)
                transform.localScale += new Vector3(velocidadCrecimiento, velocidadCrecimiento, 0) * Time.deltaTime;
            if (transform.position.y >= alturaMaxima && transform.localScale.x >= tamanoMaximo)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 1); //Mueve el objeto a Z=1
                transform.localScale = new Vector3(tamanoMaximo, tamanoMaximo, 0);
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1);
            }
        }
        if (transform.position.z == primerPlano) //esta en primer plano
        {
            transform.Translate(Vector3.down * Time.deltaTime * VelocidadDown);
            if (transform.position.y <= alturaMin)
            {
                gameObject.SetActive(false);
                /*transform.position = new Vector3(transform.position.x, puntoDeAparicion, 0); //Mueve el objeto a Z=0 e inicializa para volver a empezar
                transform.localScale = new Vector3(1, 1, 0);
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);*/

            }
        }
    }

    void MovientoLateral() {
        if (Direccion == 0){//el obstaculo va hacia la derecha
            if (transform.position.x < PuntoD) //no ha llegado al punto de la derecha
            {
                transform.Translate(Vector3.right * Time.deltaTime * VelocidadLateral);
            }
            else Direccion = 1; //va en direccion derecha y llego al punto maximo por lo que cambia de direccion
        }

        if (Direccion == 1)
        {//el obstaculo va hacia la izquieda
            if (transform.position.x > PuntoI) //no ha llegado al punto de la derecha
            {
                transform.Translate(Vector3.left * Time.deltaTime * VelocidadLateral);
            }
            else Direccion = 0; //va en direccion derecha y llego al punto maximo por lo que cambia de direccion
        }

    }


    }
