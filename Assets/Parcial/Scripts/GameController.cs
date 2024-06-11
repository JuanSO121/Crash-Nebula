using UnityEngine;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Por ejemplo, presiona 'S' para comenzar el juego
        {
            EventManager.onStartGame?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D)) // Por ejemplo, presiona 'D' para simular la muerte del jugador
        {
            EventManager.PlayerDeath();
        }
    }
}
