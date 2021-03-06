﻿using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{
    public int puntuacion, indiceTrasero, indiceFrontal;                                            //bloqueTrasero es un indice auxiliar para ver que bloque de obstaculos es el seleccionado para empezar en segundo plano

    public GameObject bloqueTrasero, bloqueFrontal;
    public GameObject Obstaculo, aleatorios;

    bool creandoAletario=false;                                                                     //Usado en la corrutina de crear obstaculos aleatorios y sirve para que cree 1 por 1 en el tiempo establecido
    private void Start()                                                                            //Cuando inicia...
    {
        indiceTrasero = Random.Range(0, transform.childCount);                                      //Asigna un numero random a indiceTrasero entre 0 y el numero de hijos de MapController(los hijos son bloques de obstaculos)
        bloqueTrasero = gameObject.transform.GetChild(indiceTrasero).gameObject;
    }

    void Update()
    {
        if (bloqueTrasero != null)
            ActivarBloque(bloqueTrasero);                                                           //Activa el bloqueTrasero

        if (estaPrimerPlano(bloqueTrasero))                                                         //Si el bloque trasero pasa totalmente a primer plano...
        {

            indiceFrontal = indiceTrasero;                                                          //indiceFrontal es igual a IndiceTrasero
            while (indiceTrasero == indiceFrontal)                                                  //Mientras indiceTrasero sea igual a indiceFrontal
                indiceTrasero = Random.Range(0, transform.childCount);                              //Asigna un indice al azar a indiceTrasero

            if (bloqueFrontal == null)                                                              //Si BloqueFrontal es nulo
            {
                bloqueFrontal = gameObject.transform.GetChild(indiceFrontal).gameObject;            
                bloqueTrasero = null;
            }        
            

            
            if(bloqueTrasero==null)
            bloqueTrasero = gameObject.transform.GetChild(indiceTrasero).gameObject;
        }

        if(bloqueFrontal != null)
            if(VerificarBloque(bloqueFrontal))
                bloqueFrontal = null;

        if (bloqueFrontal != null && bloqueTrasero != null)
        {
            if (aleatorios.transform.childCount < 4)
            {
                if (creandoAletario == false)                                                       //Si no esta creando un obstaculo...
                {
                    creandoAletario = true;                                                         //Indica que se esta creando un obstaculo
                    StartCoroutine(ObstaculosAleatorios(1));                                        //Empezar un hilo para crear un obstaculo
                }
            }
        }

    }

    void ActivarBloque(GameObject bloque)                                                           //funcion para activar el bloque de obstaculos
    {
        for (int j = 0; j < bloque.transform.childCount; j++)                                       //por cada obstaculo del bloque de obstaculos...
        {
            if (bloque.transform.GetChild(j).gameObject.activeSelf == false)                        //si no esta activo   
                bloque.transform.GetChild(j).gameObject.SetActive(true);                            //activar dicho obstaculo en el bloque 
        }
        if (bloque.activeSelf == false)                                                             //Si el Bloque de obstaculos no esta activado...
            bloque.SetActive(true);                                                                 //activa el bloque de obstaculos

    }

    bool estaPrimerPlano(GameObject bloque)                                                          //Verificar si el bloque tiene algun hijo en segundo plano
    {
        int aux = 0, j;                                                                             //variable auxiliar para contar el numero de obstaculos en segundo plano en el bloque
        for (j = 0; j < bloque.transform.childCount; j++)                                           //por cada obstaculo del bloque de obstaculos...
        {
            if (bloque.transform.GetChild(j).gameObject.transform.position.z==0)                    //si el obstaculo esta en segundo plano...
                aux++;                                                                              //suma uno a aux para contar los hijos en segundo plano
        }
        if (aux == 0)
        {                                                                                           //si aux es igual a 0...(significa que no hay hijos en segundo plano)
            return true;                                                                            //si no tiene hijos en segundo plano devuelve true
        }
        return false;                                                                               //si tiene hijos en segundo plano devuelve false
    }


    bool VerificarBloque(GameObject bloque)                                                         //Verificar si el bloque tiene algun hijo activo
    {
        int aux = 0, j;                                                                             //variable auxiliar para contar el numero de obstaculos activos en el bloque
        for (j = 0; j < bloque.transform.childCount; j++)                                           //por cada obstaculo del bloque de obstaculos
            if (bloque.transform.GetChild(j).gameObject.activeInHierarchy)                          //si el obstaculo esta activo...
                aux++;                                                                              //suma uno a aux para contar los hijos activos


        if (aux == 0) {                                                                             //si aux es igual a 0...(significa que no hay hijos activos)
            bloque.SetActive(false);                                                                //El bloque se desactiva
            for (j = 0; j < bloque.transform.childCount; j++)                                       //por cada hijo del bloque de obstaculos
                bloque.transform.GetChild(j).gameObject.SetActive(true);                            //activar el hijo para un futuro uso cuando se vuelva a activar elbloque de obstaculos
            return true;                                                                            //si no tiene hijos activos devuelve true
        }
        return false;
    }

    IEnumerator ObstaculosAleatorios(float tiempo)
    {
        for (float t = Time.time; Time.time - t < tiempo;)                                                //tiempo que tarda en no hacer nada
            yield return null;
        GameObject obstaculo =Instantiate(Obstaculo, new Vector3(Random.Range(-3, 3), -6, 0), Quaternion.identity,aleatorios.transform);
        obstaculo.SetActive(true);
        obstaculo.GetComponent<Obstaculo>().aleatorio = true;
        creandoAletario = false;
        yield return null;


    }

}


    

 