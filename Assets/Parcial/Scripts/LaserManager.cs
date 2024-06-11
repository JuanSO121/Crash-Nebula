using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public LaserPlayer laser; // Referencia al componente LaserPlayer
    public KeyCode fireKey = KeyCode.Space; // Tecla para disparar el láser

    void Update()
    {
        // Si la tecla de disparo está siendo presionada
        if (Input.GetKey(fireKey))
        {
            laser.FireLaser();
        }

        // Si se suelta la tecla de disparo
        if (Input.GetKeyUp(fireKey))
        {
            laser.StopLaser();
        }
    }
}
