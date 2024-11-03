using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen2 : MonoBehaviour
{
    public string nextSceneName; // Nome da próxima fase a ser carregada
    public Text instructionsText; // Referência ao campo de texto para as instruções
    public string instructionsMessage = "Pressione 'E' para interagir com o livro e avançar."; // Mensagem de instruções

    private void Start()
    {
        // Exibe a mensagem de instruções
        if (instructionsText != null)
        {
            instructionsText.text = instructionsMessage;
        }
    }

    private void Update()
    {
        // Verifica se a tecla ESC foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Nome da próxima cena não definido!");
        }
    }
}

