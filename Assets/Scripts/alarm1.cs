using UnityEngine;
using System.Collections;

public class AlarmCollisionAudio : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip collisionSound; // Áudio a ser reproduzido
    private AudioSource audioSource; // Componente AudioSource
    private bool hasPlayedAudio = false; // Flag para verificar se o áudio já foi tocado
    public bool isTriggerEnabled = true; // Flag para habilitar/desabilitar a função de trigger

    private SpriteRenderer spriteRenderer; // Referência ao componente SpriteRenderer
    private UnityEngine.Rendering.Universal.Light2D alarmLight; // Referência ao componente Light2D

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Verifica se o AudioSource está atribuído, se não estiver, cria um novo.
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Obtém o componente SpriteRenderer para poder ocultar/mostrar a sprite
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Obtém o componente Light2D para controlar a luz
        alarmLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    private void Update()
    {
        // Controla a visibilidade da sprite e a ativação da luz
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = isTriggerEnabled; 
        }

        if (alarmLight != null)
        {
            alarmLight.enabled = isTriggerEnabled; 
        }
    }

    public void Button1Pressed()
    {
        StartCoroutine(DeactivateCamera1(5f));
    }

    private IEnumerator DeactivateCamera1(float duration)
    {
        isTriggerEnabled = false; // Desativa a Câmera 1
        yield return new WaitForSeconds(duration); // Espera 5 segundos
        isTriggerEnabled = true; // Reativa a Câmera 1
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o trigger está habilitado
        if (!isTriggerEnabled)
        {
            return; // Se a flag estiver desativada, não faz nada
        }

        // Verifica se a colisão foi com o jogador usando a tag "Player" e se o áudio ainda não foi tocado
        if (other.CompareTag("Player") && !hasPlayedAudio)
        {
            PlayCollisionAudio(); // Toca o áudio
        }
    }

    private void PlayCollisionAudio()
    {
        if (collisionSound != null && audioSource != null)
        {
            // Define a flag como true para evitar que o som seja tocado repetidamente
            hasPlayedAudio = true;
            GameManager.sirene = true; // Ativa a sirene
            audioSource.PlayOneShot(collisionSound); // Reproduz o áudio

            // Inicia a corrotina para esperar o término do áudio
            StartCoroutine(ResetAudioFlagAfterPlay(collisionSound.length));
        }
        else
        {
            Debug.LogWarning("AudioClip ou AudioSource não está configurado.");
        }
    }

    // Corrotina que espera o áudio terminar para habilitar a reprodução novamente
    private IEnumerator ResetAudioFlagAfterPlay(float audioDuration)
    {
        yield return new WaitForSeconds(audioDuration); // Espera o tempo do áudio
        hasPlayedAudio = false; // Reseta a flag, permitindo que o som possa tocar novamente
        GameManager.sirene = false; // Desativa a sirene após o áudio terminar
    }
}

