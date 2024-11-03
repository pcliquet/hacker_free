using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator
using UnityEngine.Rendering.Universal; // Certifique-se de incluir isso para usar Light2D

public class Button4DeactivateCamera3 : MonoBehaviour
{
    public GameObject camera3; // Referência à Camera 3
    private AlarmCollisionAudio3 alarmCollisionAudio3; // Script de controle da Camera 3
    private Light2D light2D;
    private AudioSource audioSource; // Referência ao AudioSource
    public AudioClip buttonPressAudio; // O áudio que você deseja tocar
    
    private void Start()
    {
        // Obtém o script da Camera 3
        if (camera3 != null)
        {
            alarmCollisionAudio3 = camera3.GetComponent<AlarmCollisionAudio3>();
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

            // Ativa a flag do Botão 4
            if (alarmCollisionAudio3 != null)
            {
                alarmCollisionAudio3.Button4Pressed(); // Chama o método para ativar a flag do Botão 4
            }

            // Desativa a Camera 3 por 5 segundos
            StartCoroutine(DeactivateCamera3Temporarily(5f));
        }
        else
        {
            light2D.color = Color.green; // Altera a cor para verde
            light2D.intensity = 3f; // Define a intensidade da luz para 3
        }
    }

    private IEnumerator DeactivateCamera3Temporarily(float duration)
    {
        if (alarmCollisionAudio3 != null)
        {
            alarmCollisionAudio3.DeactivateCamera(); // Chama o método para desativar a câmera
        }
        yield return new WaitForSeconds(duration); // Espera 5 segundos
        if (alarmCollisionAudio3 != null)
        {
            alarmCollisionAudio3.ActivateCamera(); // Chama o método para ativar a câmera novamente
        }
    }
}

