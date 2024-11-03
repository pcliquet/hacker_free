using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator
using UnityEngine.Rendering.Universal; // Certifique-se de incluir isso para usar Light2D

public class ButtonGDeactivateCameraG : MonoBehaviour
{
    public GameObject camera1; // Referência à Câmera que será desativada
    private Light2D light2D; // Referência ao componente Light2D
    private AudioSource audioSource; // Referência ao AudioSource
    public AudioClip buttonPressAudio; // O áudio que você deseja tocar
    public float tempo; // Tempo para manter a câmera desativada

    private void Start()
    {
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
        }
    }

    private IEnumerator DeactivateCamera1Temporarily(float seconds)
    {
        camera1.SetActive(false); // Desativa a Câmera
        yield return new WaitForSeconds(seconds); // Espera o tempo especificado
        camera1.SetActive(true);  // Reativa a Câmera

        // Restaura a cor da luz e intensidade após a desativação da câmera
        if (light2D != null)
        {
            light2D.color = Color.green; // Restaura a cor original
            light2D.intensity = 1f; // Restaura a intensidade original
        }
    }
}

