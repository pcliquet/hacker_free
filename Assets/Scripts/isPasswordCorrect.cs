using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variáveis globais
    public static bool isPasswordCorrect = false;
    public static bool sirene = false;

    // Certifique-se de que o GameManager persista entre cenas
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(sirene == true){
        Debug.Log("Sirene tocando");
        }
    }
}

