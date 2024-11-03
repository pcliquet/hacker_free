using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    private void Awake()
    {
        // Se já houver uma instância de música, destrói este novo objeto
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // Define esta instância como a única e a preserva entre as cenas
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

