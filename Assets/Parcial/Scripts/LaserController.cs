using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laserPrefab; // Referencia al prefab de láser
    public KeyCode fireKey = KeyCode.Space; // Tecla para disparar el láser

    void Update()
    {
        // Si se presiona la tecla Espacio
        if (Input.GetKeyDown(fireKey))
        {
            // Instanciar el prefab de láser en la posición y rotación del objeto que contiene este script
            Instantiate(laserPrefab, transform.position, transform.rotation);
        }
    }
}