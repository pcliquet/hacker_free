using UnityEngine;

public class BustedSceneController : MonoBehaviour
{
    private void Update()
    {
        // Verifica se a tecla ESC foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC pressed in Busted scene.");
            GameManager2.Instance.RestartLastLevel();
        }
    }
}

