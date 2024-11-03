using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public string nextSceneName; // Nome da próxima fase a ser carregada
    public Text instructionsText; // Referência ao campo de texto para as instruções
    public string instructionsMessage = "Pressione 'E' para interagir com o livro e avançar."; // Mensagem de instruções
    public float loadDuration = 10f; // Duração da tela de carregamento em segundos

    private void Start()
    {
        // Exibe a mensagem de instruções
        if (instructionsText != null)
        {
            instructionsText.text = instructionsMessage;
        }
        
        // Inicia o carregamento da próxima fase após o tempo especificado
        Invoke("LoadNextScene", loadDuration);
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

