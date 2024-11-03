using UnityEngine;
using UnityEngine.SceneManagement;

public class BookInteraction : MonoBehaviour
{
    public string nextSceneName; // Nome da próxima fase que será carregada

    private bool isPlayerNearby = false; // Verifica se o jogador está próximo do livro

    private void Update()
    {
        // Verifica se o jogador está próximo e pressiona a tecla "E"
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("Carregando a próxima fase: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Nome da próxima cena não definido!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador entrou na área do livro
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Jogador perto do livro. Pressione 'E' para interagir.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o jogador saiu da área do livro
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Jogador saiu da área do livro.");
        }
    }
}

