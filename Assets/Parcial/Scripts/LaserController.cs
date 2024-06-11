using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laserPrefab; // Referencia al prefab de l�ser
    public KeyCode fireKey = KeyCode.Space; // Tecla para disparar el l�ser

    void Update()
    {
        // Si se presiona la tecla Espacio
        if (Input.GetKeyDown(fireKey))
        {
            // Instanciar el prefab de l�ser en la posici�n y rotaci�n del objeto que contiene este script
            Instantiate(laserPrefab, transform.position, transform.rotation);
        }
    }
}