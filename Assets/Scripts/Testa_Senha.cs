using UnityEngine;
using TMPro; // Se estiver usando TextMeshPro
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class PasswordInteraction : MonoBehaviour
{
    public GameObject interactionCanvas; // Referência ao Canvas da interação
    public TMP_InputField passwordInputField; // Campo de input para a senha
    public TMP_Text feedbackText; // Texto para exibir o feedback ao jogador
    public Laser_porta laser; // Referência ao script Laser_porta

    public string correctPassword = "AC"; // A senha correta

    private void Start()
    {
        // Limpa o feedback no início
        feedbackText.text = ""; 
    }

    private void Update()
    {
        // Verifica se o jogador pressionou "Esc" para fechar o prompt
        if (Input.GetKeyDown(KeyCode.Escape) && interactionCanvas.activeSelf)
        {
            UnloadPasswordScene(); // Fecha a cena "Prompt" sem recarregar o jogo
        }

        // Verifica se o jogador pressionou "Enter" após digitar a senha
        if (Input.GetKeyDown(KeyCode.Return) && interactionCanvas.activeSelf)
        {
            CheckPassword();
        }
    }

        private void CheckPassword()
        {
            if (passwordInputField != null)
            {
                string enteredPassword = passwordInputField.text;

                if (enteredPassword == correctPassword)
                {
                    feedbackText.text = "root@security_door-inspirion:~# login successful";

                    GameManager.isPasswordCorrect = true; // Define a variável global como true
                    UnloadPasswordScene();
                }
                else
                {
                    feedbackText.text = "root@security_door-inspirion:~# invalid passwd";
                }
            }
        }

    private void UnloadPasswordScene()
    {
        // Descarrega a cena "Prompt" para voltar ao jogo sem reiniciar
        SceneManager.UnloadSceneAsync("Prompt");
    }

    private void ClearInputAndFeedback()
    {
        if (passwordInputField != null)
        {
            passwordInputField.text = ""; // Limpa o campo de input
        }

        if (feedbackText != null)
        {
            feedbackText.text = ""; // Limpa o feedback
        }
    }
}

