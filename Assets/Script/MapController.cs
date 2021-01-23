using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject Bloque1, Bloque2, Bloque3, Bloque4, Bloque5, Bloque6, Bloque7;
    GameObject BloqueActual;
    int i=0;                                                                                        //i es un indice auxiliar para ver que bloque sigue
    void Update()
    {
        switch (i)                                                                                  //Activa el bloque de obstaculos segun i
        {
            case 0:
                i++;
                break;
            case 1:
                ActivarBloque(Bloque1);
                break;
            case 2:
                ActivarBloque(Bloque2);
                break;
            case 3:
                ActivarBloque(Bloque3);
                break;
            case 4:
                ActivarBloque(Bloque4);
                break;
            case 5:
                ActivarBloque(Bloque5);
                break;
            case 6:
                ActivarBloque(Bloque6);
                break;
            case 7:
                ActivarBloque(Bloque7);
                break;
        }

    }

    void ActivarBloque(GameObject Bloque)                                                           //funcion para activa el bloque de obstaculos
    {
        if (Bloque.activeSelf == false)                                                             //Si el Bloque de obstaculos no esta activado...
            Bloque.SetActive(true);                                                                 //activa el bloque de obstaculos
        if (VerificarBloque(Bloque) == true)                                                        //si el bloque no tienen hijos activos...
            i++;                                                                                    //Asigna un nuevo valor a i
        if (i > 6)  
            i = 0;
    }

    bool VerificarBloque(GameObject Bloque)                                                         //Verificar si el bloque tiene algun hijo activo
    {
        int aux=0, j;                                                                               //variable auxiliar para contar el numero de obstaculos activos en el bloque
        for (j = 0; j < Bloque.transform.childCount; j++)                                           //por cada obstaculo del bloque de obstaculos
        {
            if (Bloque.transform.GetChild(j).gameObject.activeInHierarchy)                          //si el obstaculo esta activo...
                aux++;                                                                              //suma uno a aux para contar los hijos activos
        }
        if (aux==0){                                                                                //si aux es igual a 0...(significa que no hay hijos activos)
            Bloque.SetActive(false);                                                                //El bloque se desactiva
            for (j = 0; j < Bloque.transform.childCount; j++)                                       //por cada hijo del bloque de obstaculos
                Bloque.transform.GetChild(j).gameObject.SetActive(true);                            //activar el hijo para un futuro uso cuando se vuelva a activar elbloque de obstaculos
            return true;                                                                            //si no tiene hijos activos devuelve true
        }
        return false;                                                                               //si tiene hijos activos devuelve false
    }


}




