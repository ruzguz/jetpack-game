using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float puntoDeAparicion, puntoD, puntoI, velocidadCrecimiento, tamano;                                                           //Punto de aparicion es el punto donde aparece en pantalla el obstaculo cuando se activa
                                                                                                            //Desplazamiento es cuanto se deplazara en el eje X los objectos con movimiento lateral


    public bool seMueveLateral;                                                                             //SeMueveLateral determina si el obstaculo tiene desplazamiento lateral
                                                                                                            //velocidadDown es la velocidad de bajada y define el movimiento de subida y el movimiento lateral
    float velocidadDown, velocidadUp, velocidadLateral, direccion, velocidadTransparencia,                  //direccion es un auxiliar para determinar hacia donde se esta moviendo un obstaculo con movimiento lateral
        tamanoMaximo, alturaMaxima, alturaMin;                                       //velocidadTransparencia la rapidez que tiene para quitar la transparencia del objeto a medida que sube en el segundo plano
                                                                                                            //velocidadCrecimiento es la rapidez con la que crece el objeto a medida que sube en el segundo plano
                                                                                                            //puntoD y puntoI son el punto derecho o izquierdo en el eje x donde los movimientos laterales deben llegar

    const int primerPlano=1, segundoPlano=0;                                                                //variables que constantes para facilitar el entendimiento del codigo

                                                                                                            //La velocidad de transparencia es igual distancia/100
                                                                                                            //distancia es igual a punto de aparicion menos altura maxima
    private void Start()
    {
        velocidadDown = 1;                                                                                  //Se dan valor a las variables
        tamanoMaximo = 2;
        alturaMaxima = 6;
        alturaMin = -alturaMaxima;
        velocidadUp = velocidadDown / 2;
        if (seMueveLateral == true)                                                                         //Si el obstaculo tiene movimiento lateral...
            velocidadLateral = velocidadDown;                                                               //La velocidad de movimiento lateral es igual al de bajada
        else                                                                                                //Si el obstaculo no tiene movimiento lateral...
            puntoD = puntoI = 0;                                                                            //Se dan valor de 0 para prevenir errores        

        velocidadCrecimiento = tamanoMaximo/(alturaMaxima-puntoDeAparicion);

        //v                  = 2/(6-4.5)
        //                   = 2/(1.5)
    }

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, puntoDeAparicion, 0);                        //Mueve el objeto a Z=0 
        transform.localScale = new Vector3(0, 0, 0);                                                        //Convierte su escala en la adecuada para el segundo plano
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r,            //El objecto se pone en su transparencia maxima
            GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);
    }

    void Update()
    {
        if (!(puntoD == 0 && puntoI == 0))
            MovimientoLateral();
        velocidadTransparencia = ((255 / (Mathf.Abs(alturaMaxima - puntoDeAparicion + 2))) / 100);
        if (transform.position.z == segundoPlano)                                                           //esta en segundo plano 
        {
            if (transform.position.y < alturaMaxima)                                                        //llego al punto maximoo y se prepara para entrar al primer plano
            {
                transform.Translate(Vector3.up * Time.deltaTime * velocidadUp);
                if (GetComponent<SpriteRenderer>().color.a < 0.8)
                    GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 
                        velocidadTransparencia * Time.deltaTime * velocidadUp);
            }
            if (transform.localScale.x < tamanoMaximo)

                //Distancia es igual a punto de aparicion - punto maximo = 6 
                //Tamano de crecimiento = 2/6


                tamano = tamanoMaximo / (alturaMaxima - transform.position.y);
                transform.localScale = new Vector3(tamano,tamano, 0) * Time.deltaTime; //Tamano es igual a la distancia que le falta
            if (transform.position.y >= alturaMaxima && transform.localScale.x >= tamanoMaximo)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 1);            //Mueve el objeto a Z=1
                transform.localScale = new Vector3(tamanoMaximo, tamanoMaximo, 0);
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, 
                    GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1);
            }
        }
        if (transform.position.z == primerPlano) //esta en primer plano
        {
            transform.Translate(Vector3.down * Time.deltaTime * velocidadDown);
            if (transform.position.y <= alturaMin)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void MovimientoLateral() {
        if (direccion == 0){//el obstaculo va hacia la derecha
            if (transform.position.x < puntoD) //no ha llegado al punto de la derecha
            {
                transform.Translate(Vector3.right * Time.deltaTime * velocidadLateral);
            }
            else direccion = 1; //va en direccion derecha y llego al punto maximo por lo que cambia de direccion
        }
        if (direccion == 1)
        {//el obstaculo va hacia la izquieda
            if (transform.position.x > puntoI) //no ha llegado al punto de la derecha
            {
                transform.Translate(Vector3.left * Time.deltaTime * velocidadLateral);
            }
            else direccion = 0; //va en direccion derecha y llego al punto maximo por lo que cambia de direccion
        }
    }
}
