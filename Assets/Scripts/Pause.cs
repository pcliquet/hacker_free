using UnityEngine;
using UnityEngine.SceneManagement; // Para sair para o menu principal

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // O Canvas da tela de pause
    public AudioSource backgroundMusic; // Referência à música de fundo
    private bool isPaused = false; // Flag para verificar se o jogo está pausado
    private bool isMuted = false; // Flag para verificar se a música está mutada

    void Update()
    {
        // Verifica se o jogador pressionou "Esc" para pausar/despausar o jogo
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                Resume(); // Se já está pausado, resume o jogo
            }
            else
            {
                Pause(); // Caso contrário, pausa o jogo
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Esconde o menu de pause
        Time.timeScale = 1f; // Despausa o tempo de jogo
        isPaused = false; // Marca como não pausado
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true); // Exibe o menu de pause
        Time.timeScale = 0f; // Pausa o tempo de jogo
        isPaused = true; // Marca como pausado
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Retorna o tempo normal antes de sair
        SceneManager.LoadScene("MainMenu"); // Carrega a cena do menu principal
    }

    public void QuitGame()
    {
        Debug.Log("Sair do jogo...");
        Application.Quit(); // Fecha o jogo (funciona no build, não no editor)
    }

    // Método para mutar/desmutar a música de fundo
    public void ToggleMuteMusic()
    {
        if (isMuted)
        {
            backgroundMusic.Play(); // Retoma a música
            isMuted = false; // Atualiza flag
        }
        else
        {
            backgroundMusic.Pause(); // Pausa a música
            isMuted = true; // Atualiza flag
        }
    }
}

