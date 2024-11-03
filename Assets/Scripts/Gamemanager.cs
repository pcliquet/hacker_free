using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }

    public string lastLevel; // Nome da última fase

    private void Awake()
    {
        // Garante que o GameManager seja um Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadBustedScene()
    {
        // Salva o nome da fase atual antes de ir para a cena "Busted"
        lastLevel = SceneManager.GetActiveScene().name;
        Debug.Log("Last level saved: " + lastLevel);
        SceneManager.LoadScene("Busted");
    }

    public void RestartLastLevel()
    {
        if (!string.IsNullOrEmpty(lastLevel))
        {
            Debug.Log("Returning to last level: " + lastLevel);
            SceneManager.LoadScene(lastLevel);
        }
        else
        {
            Debug.LogWarning("Last level not set. Cannot return.");
        }
    }
}

