using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    // N�mero de la escena a cargar
    [SerializeField] int sceneToLoad = 1;

    // M�todo llamado cuando se detecta una colisi�n
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisi�n es con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambiar a la nueva escena por n�mero
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
