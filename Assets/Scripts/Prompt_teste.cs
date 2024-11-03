using UnityEngine;
using UnityEngine.SceneManagement; // Importar para trocar de cena

public class InteractionPrompt : MonoBehaviour
{
    private bool isInteracting = false; // Flag para verificar se está interagindo

    private void Update()
    {
        // Verifica se o jogador pressionou a tecla "E" para interagir
        if (Input.GetKeyDown(KeyCode.E) && isInteracting)
        {
            LoadPasswordScene(); // Chama o método que carrega a cena da senha
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (other.CompareTag("Player"))
        {
            isInteracting = true; // Permite interação
            Debug.Log("Pressione 'E' para interagir");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Desativa a interação quando o jogador sair da área do collider
        if (other.CompareTag("Player"))
        {
            isInteracting = false; // Desabilita interação
        }
    }

    private void LoadPasswordScene()
    {
        // Carrega a cena de senha de forma aditiva, sem descarregar a cena atual
        SceneManager.LoadScene("Prompt", LoadSceneMode.Additive);
    }
}

