using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator
using UnityEngine.Rendering.Universal; // Certifique-se de incluir isso para usar Light2D


public class Button3DeactivateCamera2 : MonoBehaviour
{
    public GameObject camera2; // Referência à Camera 2
    private AlarmCollisionAudio2 alarmCollisionAudio2; // Script de controle da Camera 2
    private Light2D light2D;
    private AudioSource audioSource; // Referência ao AudioSource
    public AudioClip buttonPressAudio; // O áudio que você deseja tocar
    
    private void Start()
    {
        // Obtém o script da Camera 2
        if (camera2 != null)
        {
            alarmCollisionAudio2 = camera2.GetComponent<AlarmCollisionAudio2>();
        }
        light2D = GetComponent<Light2D>(); 
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o Player encostou no botão
        if (other.CompareTag("Player"))
        {
            GameManager.sirene = false;

            if (audioSource != null && buttonPressAudio != null)
            {
                audioSource.PlayOneShot(buttonPressAudio); // Toca o áudio uma vez
            }
            if (light2D != null)
            {
                light2D.color = Color.red; // Altera a cor para vermelho
                light2D.intensity = 3f; // Define a intensidade da luz para 3

            }

            // Ativa a flag do Botão 3
            if (alarmCollisionAudio2 != null)
            {
                alarmCollisionAudio2.Button3Pressed(); // Chama o método para ativar a flag do Botão 3
            }

        }
        else{
                light2D.color = Color.green; // Altera a cor para vermelho
                light2D.intensity = 3f; // Define a intensidade da luz para 3
            }
    }
}

