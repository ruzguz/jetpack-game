using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //public GameObject Obstaculo;
    public int velocidad;

    public GameObject Obstaculo1, Obstaculo2, Obstaculo3;
    int aux1=0, aux2=0, aux3=0;
    // Update is called once per frame
    void Update()
    {

        MoverObstaculo(Obstaculo1,aux1);aux1 = 1;
        /*if (Obstaculo1.transform.position.y >= 0 && aux2 == 0)
        {
            MoverObstaculo(Obstaculo2,aux2);
            aux2 = 1;    
        }
        if (Obstaculo2.transform.position.y >= -1 && aux3==0)
        {
            MoverObstaculo(Obstaculo3,aux3);
            aux3 = 1;
        }*/
    }

    public void MoverObstaculo(GameObject Obstaculo, int aux){
        if (Obstaculo.transform.position.z == 0) //esta en segundo plano 
        {
            if (Obstaculo.transform.position.y < 4.5f)
                Obstaculo.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * velocidad * 1f);
            if (Obstaculo.transform.localScale.x < 4)
                Obstaculo.transform.localScale += new Vector3(1.05f, 1.05f, 0) * Time.deltaTime;
            if (Obstaculo.transform.position.y >= 4.5f && Obstaculo1.transform.localScale.x >= 4) //llego al punto alto y se prepara que entre al primer plano
            {
                Obstaculo.transform.position = new Vector3(Obstaculo1.transform.position.x, Obstaculo1.transform.position.y, 1); //Mueve el objeto a Z=1
                Obstaculo.transform.localScale = new Vector3(4, 4, 0);
            }
        }
        if (Obstaculo.transform.position.z == 1) //esta en primer plano
        {
            Obstaculo.transform.Translate(Vector3.down * Time.deltaTime * velocidad);
            if (Obstaculo.transform.position.y <= -6)
            {
                Obstaculo.transform.position = new Vector3(Random.Range(-2.6f, 2.6f), -6, 0); //Mueve el objeto a Z=0
                Obstaculo.transform.localScale = new Vector3(1, 1, 0);
                aux = 0;
            }
        }
    }

}



