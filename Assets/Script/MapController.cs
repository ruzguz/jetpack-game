using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject Bloque1, Bloque2, Bloque3, Bloque4, Bloque5, Bloque6, Bloque7;
    GameObject BloqueActual;
    int i=0;
    void Update()
    {
        switch (i)
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
       
    

    bool VerificarBloque(GameObject Bloque) //Verificar si el bloque tiene algun hijo activo
    {
        for (int j = 0; j < Bloque.transform.childCount; j++)
        {
            if (!Bloque.transform.GetChild(j).gameObject.activeInHierarchy)
            {
                Bloque.SetActive(false);
                return true; //si no tiene hijos devuelve true
            }
        }
        return false; //si tiene hijos devuelve false
    }

    void ActivarBloque(GameObject Bloque)
    {
        Bloque.SetActive(true);
        if (VerificarBloque(Bloque) == true)
        {//significa que el bloque no tienen hijos activos\
            i++;
        }
        if (i > 7)
            i = 0;
    }
}




