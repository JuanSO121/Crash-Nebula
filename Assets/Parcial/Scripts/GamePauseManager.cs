using UnityEngine;
using UnityEngine.SceneManagement; // Aseg�rate de incluir este namespace

public class GamePauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel; // El panel del men� de pausa

    private bool isGamePaused = false;

    private void Awake()
    {
        // Inicialmente, aseg�rate de que el men� de pausa est� desactivado
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

    // M�todo para continuar el juego desde el men� de pausa
    public void ContinueGame()
    {
        isGamePaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego
    }

    // M�todo para volver al men� principal
    public void GoToMainMenu()
    {
        // Aseg�rate de reanudar el tiempo antes de cambiar de escena
        Time.timeScale = 1f;

        // Carga la escena con �ndice 0 (men� principal)
        SceneManager.LoadScene(0);
    }
}
