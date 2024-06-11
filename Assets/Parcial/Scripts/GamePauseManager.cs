using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de incluir este namespace

public class GamePauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel; // El panel del menú de pausa

    private bool isGamePaused = false;

    private void Awake()
    {
        // Inicialmente, asegúrate de que el menú de pausa esté desactivado
        pauseMenuPanel.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.onPauseGame += TogglePauseMenu;
    }

    private void OnDisable()
    {
        EventManager.onPauseGame -= TogglePauseMenu;
    }

    private void Update()
    {
        // Comprueba si se ha presionado la tecla Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.PauseGame();
        }
    }

    private void TogglePauseMenu()
    {
        // Alterna el estado del juego entre pausado y no pausado
        isGamePaused = !isGamePaused;
        pauseMenuPanel.SetActive(isGamePaused);

        // Pausa o reanuda el juego configurando la escala de tiempo
        Time.timeScale = isGamePaused ? 0f : 1f;
    }

    // Método para continuar el juego desde el menú de pausa
    public void ContinueGame()
    {
        isGamePaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego
    }

    // Método para volver al menú principal
    public void GoToMainMenu()
    {
        // Asegúrate de reanudar el tiempo antes de cambiar de escena
        Time.timeScale = 1f;

        // Carga la escena con índice 0 (menú principal)
        SceneManager.LoadScene(0);
    }
}
