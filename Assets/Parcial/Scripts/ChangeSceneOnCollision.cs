using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    // Número de la escena a cargar
    [SerializeField] int sceneToLoad = 1;

    // Método llamado cuando se detecta una colisión
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambiar a la nueva escena por número
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
