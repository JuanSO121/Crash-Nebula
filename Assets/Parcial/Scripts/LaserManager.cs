using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public LaserPlayer laser; // Referencia al componente LaserPlayer
    public KeyCode fireKey = KeyCode.Space; // Tecla para disparar el l�ser

    void Update()
    {
        // Si la tecla de disparo est� siendo presionada
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
