using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //public GameObject Obstaculo;
    public int velocidadUp,VelocidadDown,puntoDeAparicion;
    public float velocidadTransparencia, tamanoMaximo, alturaMaxima, alturaMin, velocidadCrecimiento;

    public GameObject Obstaculo1, Obstaculo2, Obstaculo3;
    int aux1 = 0;
    int primerPlano = 1, segundoPlano = 0, puntoRetorno = 6;


    //La velocidad de transparencia es igual distancia/100
    //distancia es igual a punto de aparicion menos altura maxima

    // Update is called once per frame
    void Start()
    {
    }
    void Update()
    {
        velocidadTransparencia = ((255/(Mathf.Abs(alturaMaxima - puntoDeAparicion+2)))/100);
        MoverObstaculo(Obstaculo1);
    }

    public void MoverObstaculo(GameObject Obstaculo){
        if (Obstaculo.transform.position.z == segundoPlano) //esta en segundo plano 
        {
            if (Obstaculo.transform.position.y < puntoRetorno ) //llego al punto alto y se prepara que entre al primer plano
            { 
                Obstaculo.transform.Translate(Vector3.up * Time.deltaTime * velocidadUp);
                if (Obstaculo1.GetComponent<SpriteRenderer>().color.a < 0.8)
                Obstaculo.GetComponent<SpriteRenderer>().color += new Color(0,0,0,velocidadTransparencia*Time.deltaTime*velocidadUp);
            }

            if (Obstaculo.transform.localScale.x < tamanoMaximo)
                Obstaculo.transform.localScale += new Vector3(velocidadCrecimiento, velocidadCrecimiento, 0) * Time.deltaTime;
            if (Obstaculo.transform.position.y >= alturaMaxima && Obstaculo1.transform.localScale.x >= tamanoMaximo) 
            {
                Obstaculo.transform.position = new Vector3(Obstaculo1.transform.position.x, Obstaculo1.transform.position.y, 1); //Mueve el objeto a Z=1
                Obstaculo.transform.localScale = new Vector3(tamanoMaximo, tamanoMaximo, 0);
                Obstaculo.GetComponent<SpriteRenderer>().color = new Color(Obstaculo.GetComponent<SpriteRenderer>().color.r, Obstaculo.GetComponent<SpriteRenderer>().color.g, Obstaculo.GetComponent<SpriteRenderer>().color.b, 1);
            }
        }
        if (Obstaculo.transform.position.z == primerPlano) //esta en primer plano
        {
            Obstaculo.transform.Translate(Vector3.down * Time.deltaTime * VelocidadDown);
            if (Obstaculo.transform.position.y <= alturaMin)
            {
                Obstaculo.transform.position = new Vector3(Random.Range(-2.6f, 2.6f), puntoDeAparicion, 0); //Mueve el objeto a Z=0 e inicializa para volver a empezar
                Obstaculo.transform.localScale = new Vector3(1, 1, 0);
                Obstaculo.GetComponent<SpriteRenderer>().color = new Color(Obstaculo.GetComponent<SpriteRenderer>().color.r, Obstaculo.GetComponent<SpriteRenderer>().color.g, Obstaculo.GetComponent<SpriteRenderer>().color.b, 0);

            }
        }
    }

}



