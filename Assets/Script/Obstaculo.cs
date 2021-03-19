using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public GameObject GameManager;

    public float puntoDeAparicion, puntoD, puntoI, tamano, vida;                                                  //Punto de aparicion es el punto donde aparece en pantalla el obstaculo cuando se activa
                                                                                                            //Desplazamiento es cuanto se deplazara en el eje X los objectos con movimiento lateral


    public bool seMueveLateral,lanzador,aleatorio=false;                                                                             //SeMueveLateral determina si el obstaculo tiene desplazamiento lateral
    public float velocidadDown;                                                                                                        //velocidadDown es la velocidad de bajada y define el movimiento de subida y el movimiento lateral
    float  velocidadUp, velocidadLateral, direccion, velocidadTransparencia,                  //direccion es un auxiliar para determinar hacia donde se esta moviendo un obstaculo con movimiento lateral
        tamanoMaximo, alturaMaxima, alturaMin, velocidadRotacion;                                           //velocidadTransparencia la rapidez que tiene para quitar la transparencia del objeto a medida que sube en el segundo plano
                                                                                                            //velocidadCrecimiento es la rapidez con la que crece el objeto a medida que sube en el segundo plano
                                                                                                            //puntoD y puntoI son el punto derecho o izquierdo en el eje x donde los movimientos laterales deben llegar

    const int primerPlano=1, segundoPlano=0;                                                                //variables que constantes para facilitar el entendimiento del codigo

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        actualizarVelocidad();                                                        //La velocidad de movimiento lateral es igual al de bajada
        vida = 5;
        velocidadDown = 1;                                                                                  //Se inicializan las variables
        tamanoMaximo = 2;
        alturaMaxima = 6;
        alturaMin = -alturaMaxima;
        velocidadUp = velocidadDown / 2;
        if (seMueveLateral == true)                                                                         //Si el obstaculo tiene movimiento lateral...
            velocidadLateral = velocidadDown/2;                                                               //La velocidad de movimiento lateral es igual al de bajada
        else                                                                                                //Si el obstaculo no tiene movimiento lateral...
            puntoD = puntoI = 0;                                                                            //Se dan valor de 0 para prevenir errores        
        velocidadTransparencia = ((255 / (Mathf.Abs(alturaMaxima - puntoDeAparicion + 2))) / 100);          //Calcula la velocidad en la que debe quitar la opacidad mientras sube 

        if (Random.Range(0, 2)==0)
            velocidadRotacion = Random.Range(50, 100);
        else
            velocidadRotacion = Random.Range(-50, -100);

    }

    void OnEnable()
    {
        transform.localPosition = new Vector3(transform.position.x, puntoDeAparicion, 0);                        //Mueve el objeto a Z=0 
        transform.localScale = new Vector3(0, 0, 0);                                                      //Convierte su escala en la adecuada para el segundo plano
        //transform.localScale = new Vector3(2, 2, 2);                                                        //Convierte su escala en la adecuada para el segundo plano
                                                                                                            //El objecto se pone en su transparencia maxima
        GetComponentInChildren<SpriteRenderer>().color = new Color(GetComponentInChildren<SpriteRenderer>().color.r,
            GetComponentInChildren<SpriteRenderer>().color.g, GetComponentInChildren<SpriteRenderer>().color.b, 0);   



        
    }

    void Update()
    {
        actualizarVelocidad();
        if (!(puntoD == 0 && puntoI == 0))                                                                  //Si el Obstaculo tiene movimiento lateral...
            MovimientoLateral();                                                                            //llama la funcion para hacer movimiento lateral

        if(seMueveLateral==false)
        transform.GetChild(0).transform.Rotate(new Vector3(0, 0, velocidadRotacion* Time.deltaTime));

        if (transform.position.z == segundoPlano)                                                           //esta en segundo plano 
        {
            SegundoPlano();                                                                                 //Codigo para el objeto en segundo plano
        }
        if (transform.position.z == primerPlano)                                                            //esta en primer plano
        {
            PrimerPlano();                                                                                  //codigo para objeto en el primer plano
        }
    }

    void MovimientoLateral() {
        if (direccion == 0){                                                                                //Si el obstaculo va hacia la derecha...
            if (transform.position.x < puntoD)                                                              //Si no ha llegado al punto maximo de la derecha..
            {
                transform.Translate(Vector3.right * Time.deltaTime * velocidadLateral);                     //trasladarse en direccion derecha
            }
            else direccion = 1;                                                                             //si no va en direccion derecha y llego al punto maximo por lo que cambia de direccion
        }
        if (direccion == 1)                                                                                 //Si el obstaculo va hacia la izquierda...
        {
            if (transform.position.x > puntoI)                                                              //Si no ha llegado al punto maximo de la derecha
            {
                transform.Translate(Vector3.left * Time.deltaTime * velocidadLateral);                      //trasladarse en direccion izquierda
            }
            else direccion = 0;                                                                             //va en direccion izquierda y llego al punto maximo por lo que cambia de direccion
        }
    }                                                                           

    void SegundoPlano()
    {
        transform.GetComponentInChildren<CircleCollider2D>().enabled = false;                               //Desabilita el boxcolider para evitar errores

        if (transform.position.y < alturaMaxima)                                                            //si la posicion del obstaculo es menor a la altura maxima....
        {
            transform.Translate(Vector3.up * Time.deltaTime * velocidadUp);                                 //mover el obstaculo hacia arriba

            //------------------------------------------------------------------------------ Mejorar
            if (GetComponentInChildren<SpriteRenderer>().color.a < 0.6)                                     //si el objeto tiene la tiene la transparencia menor a 0.6...
                GetComponentInChildren<SpriteRenderer>().color += new Color(0, 0, 0,
                    velocidadTransparencia * Time.deltaTime * velocidadUp);                                 //reduce la transparencia
        }

        if (transform.localScale.x < tamanoMaximo && transform.position.y > 0)
        {
            tamano = tamanoMaximo/ (alturaMaxima - transform.position.y);
            transform.localScale = new Vector3(tamano, tamano, 0);                                          //Tamano es igual a la distancia que le falta
        }
        if (transform.position.y >= alturaMaxima )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);                //Mueve el objeto a Z=1
            transform.localScale = new Vector3(tamanoMaximo, tamanoMaximo, 0);
            GetComponentInChildren<SpriteRenderer>().color = new Color(GetComponentInChildren<SpriteRenderer>().color.r,
                GetComponentInChildren<SpriteRenderer>().color.g, GetComponentInChildren<SpriteRenderer>().color.b, 1);
        }
    }                                                                                 

    void PrimerPlano()
    {
        transform.GetComponent<CircleCollider2D>().enabled = true;                                            //habilitar el colider del obstaculo
        transform.Translate(Vector3.down * Time.deltaTime * velocidadDown);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")                                                                     //Si colisiono con el player...
        {
            //explotar
            gameObject.SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseScore(-100);
        }

        if (collision.name == "LimiteInferior" && transform.position.z==1)                                                             //Si colisiono con el Limite inferior
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseScore(69);
            gameObject.SetActive(false);
            if (aleatorio == true)
            {
                Destroy(gameObject);
            }
        }
    }

    void actualizarVelocidad(){
        velocidadDown = GameManager.GetComponent<GameManager>().velocidadObstaculos;
        velocidadUp = velocidadDown / 2;
        velocidadLateral = velocidadDown/2;                                                          //La velocidad de movimiento lateral es igual al de bajada

    }

}

