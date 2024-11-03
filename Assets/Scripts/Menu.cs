using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas
using UnityEngine.UI; // Necessário para manipular UI

public class MainMenu : MonoBehaviour
{
    // Referência ao AudioSource que toca a música de fundo
    public AudioSource backgroundMusic;

    // Método chamado quando o botão "Iniciar Jogo" é pressionado
    public void StartGame()
    {
        // Carrega a cena "Game" (substitua pelo nome correto se necessário)
        SceneManager.LoadScene("Instruções");
    }

    // Método chamado quando o botão "Sair" é pressionado
    public void QuitGame()
    {
        // Sai do aplicativo
        Application.Quit();

        // Se estiver no editor, também para o playmode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Método chamado quando o botão "Mute" é pressionado
    public void ToggleMuteMusic()
    {
        // Verifica se a música está tocando
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause(); // Pausa a música
        }
        else
        {
            backgroundMusic.Play(); // Retoma a música
        }
    }
}

