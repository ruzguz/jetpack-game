using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    int i;                                                                                          //i es un indice auxiliar para ver que bloque de obstaculoses el seleccionado
    private void Start()                                                                            //Cuando inicia...
    { 
        i = Random.Range(0, transform.childCount);                                                  //Asigna un numero random a i entre 0 y el numero de hijos de MapController(los hijos son bloques de obstaculos)
    }

    void Update()
    {
        ActivarBloque(gameObject.transform.GetChild(i).gameObject);                                 //Activa el bloque con el indice i
    }

    void ActivarBloque(GameObject Bloque)                                                           //funcion para activa el bloque de obstaculos
    {
        if (Bloque.activeSelf == false)                                                             //Si el Bloque de obstaculos no esta activado...
            Bloque.SetActive(true);                                                                 //activa el bloque de obstaculos
        if (VerificarBloque(Bloque) == true)                                                        //si el bloque no tienen hijos activos...
            i = Random.Range(0, transform.childCount);                                              //Asigna un nuevo valor a i
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




