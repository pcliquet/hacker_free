using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator
using UnityEngine.Rendering.Universal; // Certifique-se de incluir isso para usar Light2D

public class Button1DeactivateCamera1 : MonoBehaviour
{
    public GameObject camera1; // Referência à Câmera 1
    public GameObject camera2; // Referência à Câmera 2
    private AlarmCollisionAudio2 alarmCollisionAudio2; 
    private Light2D light2D; // Referência ao componente Light2D
    private AudioSource audioSource; // Referência ao AudioSource
    public AudioClip buttonPressAudio; // O áudio que você deseja tocar
    public float tempo;

    private void Start()
    {
        // Obtém o script da Câmera 2
        if (camera2 != null)
        {
            alarmCollisionAudio2 = camera2.GetComponent<AlarmCollisionAudio2>(); // Certifique-se de usar AlarmCollisionAudio
        }
        
        // Obtém o componente Light2D do objeto que contém este script
        light2D = GetComponent<Light2D>();
        
        // Obtém o componente AudioSource do objeto que contém este script
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o Player encostou no botão
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DeactivateCamera1Temporarily(tempo));
            GameManager.sirene = false;
            // Toca o áudio do botão
            if (audioSource != null && buttonPressAudio != null)
            {
                audioSource.PlayOneShot(buttonPressAudio); // Toca o áudio uma vez
            }

            // Altera a cor da luz para vermelho com intensidade 3
            if (light2D != null)
            {
                light2D.color = Color.red; // Altera a cor para vermelho
                light2D.intensity = 3f; // Define a intensidade da luz para 3
            }

            // Ativa a flag do Botão 1 na Câmera 2
            if (alarmCollisionAudio2 != null)
            {
                alarmCollisionAudio2.Button1Pressed(); // Chame o método correto para ativar a flag
            }
            else{
                light2D.color = Color.green; // Altera a cor para vermelho
                light2D.intensity = 3f; // Define a intensidade da luz para 3
            }
        }
    }

    private IEnumerator DeactivateCamera1Temporarily(float seconds)
    {
        camera1.SetActive(false); // Desativa a Câmera 1
        yield return new WaitForSeconds(seconds); // Espera 5 segundos
        camera1.SetActive(true);  // Reativa a Câmera 1

        // Restaura a cor da luz e intensidade após a desativação da câmera
        if (light2D != null)
        {
            light2D.color = Color.green; // Restaura a cor original
            light2D.intensity = 1f; // Restaura a intensidade original (ajuste conforme necessário)
        }
    }
}

