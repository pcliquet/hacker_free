using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator
using UnityEngine.Rendering.Universal; // Certifique-se de incluir isso para usar Light2D


public class Button2DeactivateCamera1Permanently : MonoBehaviour
{
    public GameObject camera1; // Referência à Câmera 1
    public GameObject camera2; // Referência à Câmera 2
    private AlarmCollisionAudio2 alarmCollisionAudio2; // Script de controle da Câmera 2
    private Light2D light2D;
    private AudioSource audioSource; // Referência ao AudioSource
    public AudioClip buttonPressAudio; // O áudio que você deseja tocar




    private void Start()
    {
        // Obtém o script da Câmera 2
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
            camera1.SetActive(false); // Desativa a Câmera 1
            
            if (audioSource != null && buttonPressAudio != null)
            {
                audioSource.PlayOneShot(buttonPressAudio); // Toca o áudio uma vez
            }
            if (light2D != null)
            {
                light2D.color = Color.green; // Altera a cor para vermelho
                light2D.intensity = 3f; // Define a intensidade da luz para 3
                

            }


            // Reseta a flag do Botão 1 na Câmera 2
            if (alarmCollisionAudio2 != null)
            {
                alarmCollisionAudio2.Button2Pressed(); 
            }
        }
    }
}

