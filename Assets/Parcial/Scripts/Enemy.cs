using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target; // Referencia al objetivo del enemigo
    [SerializeField] float rotationalDamp = .2f; // Damping para la rotación
    [SerializeField] float movementSpeed = 10f; // Velocidad de movimiento del enemigo

    [SerializeField] float detectionDistance = 20f; // Distancia de detección para raycasting
    [SerializeField] float rayCastOffset = 2.5f; // Desplazamiento para los rayos de detección

    void OnEnable()
    {
        EventManager.onPlayerDeath += FindMainCamera; // Suscribirse al evento de muerte del jugador
        FindTarget(); // Intentar encontrar al `Player` al habilitar el script
    }

    void OnDisable()
    {
        EventManager.onPlayerDeath -= FindMainCamera; // Desuscribirse del evento de muerte del jugador
    }

    void Update()
    {
        if (!FindTarget())
            return; // Si no se encuentra el `Player`, no hacer nada

        PathFinding(); // Lógica de evasión de obstáculos
        Move(); // Mover al enemigo
    }

    void Move()
    {
        // Mover al enemigo hacia adelante
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void PathFinding()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero; // Desplazamiento para ajustar la dirección de evasión

        // Posiciones para lanzar los rayos alrededor del enemigo
        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        // Dibujar los rayos en la escena para depuración
        Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

        // Lógica de detección de obstáculos y ajuste de la dirección
        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance))
            raycastOffset += Vector3.right; // Ajustar la dirección hacia la derecha si se detecta un obstáculo a la izquierda

        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
            raycastOffset -= Vector3.right; // Ajustar la dirección hacia la izquierda si se detecta un obstáculo a la derecha

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
            raycastOffset -= Vector3.up; // Ajustar la dirección hacia abajo si se detecta un obstáculo arriba

        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
            raycastOffset += Vector3.up; // Ajustar la dirección hacia arriba si se detecta un obstáculo abajo

        // Ajuste más gradual de la rotación basado en el desplazamiento
        if (raycastOffset != Vector3.zero)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, transform.forward + raycastOffset, 5f * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            Turn();
        }
    }

    void Turn()
    {


        // Girar el enemigo hacia el objetivo con suavizado
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }


    bool FindTarget()
    {
        if (target == null)
        {
            // Buscar al `Player` en la escena
            GameObject temp = GameObject.FindGameObjectWithTag("Player");

            if (temp != null)
                target = temp.transform;
        }

        // Si aún no hay objetivo, retornar false
        if (target == null)
            return false;

        return true;
    }

    void FindMainCamera()
    {
        // Cambiar el objetivo al `MainCamera` cuando el jugador muere
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
}
