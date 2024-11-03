using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal; // Necessário para o uso de Light2D

public class AlarmGenericCollisionAudio : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip collisionSound; // Áudio a ser reproduzido quando o alarme é ativado
    private AudioSource audioSource; // Componente de áudio do alarme
    private bool hasPlayedAudio = false; // Flag para verificar se o áudio já foi tocado
    public bool isTriggerEnabled = true; // Flag para habilitar/desabilitar o alarme

    [Header("Visual Settings")]
    private SpriteRenderer spriteRenderer; // Componente SpriteRenderer para visualização do alarme
    private Light2D alarmLight; // Componente de iluminação para efeito visual do alarme

    private void Awake()
    {
        // Configura o AudioSource
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        // Configura o SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Configura o Light2D
        alarmLight = GetComponent<Light2D>();
    }

    private void Update()
    {
        // Controla a visibilidade da sprite e da luz com base na flag isTriggerEnabled
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = isTriggerEnabled;
        }

        if (alarmLight != null)
        {
            alarmLight.enabled = isTriggerEnabled;
        }
    }

    // Método que simula a ativação de um botão para desativar temporariamente o alarme
    public void DeactivateAlarmTemporarily(float duration)
    {
        StartCoroutine(DeactivateAlarmCoroutine(duration));
    }

    private IEnumerator DeactivateAlarmCoroutine(float duration)
    {
        isTriggerEnabled = false; // Desativa o alarme temporariamente
        yield return new WaitForSeconds(duration); // Espera o tempo especificado
        isTriggerEnabled = true; // Reativa o alarme
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o alarme está ativo e se o player entrou na área do alarme
        if (isTriggerEnabled && other.CompareTag("Player") && !hasPlayedAudio)
        {
            PlayCollisionAudio(); // Toca o áudio de colisão do alarme
        }
    }

    private void PlayCollisionAudio()
    {
        if (collisionSound != null && audioSource != null)
        {
            hasPlayedAudio = true; // Define a flag para evitar a reprodução repetida
            GameManager.sirene = true; // Ativa a sirene no GameManager (se aplicável)
            audioSource.PlayOneShot(collisionSound); // Reproduz o áudio do alarme

            // Inicia a corrotina para resetar a flag quando o áudio terminar
            StartCoroutine(ResetAudioFlagAfterPlay(collisionSound.length));
        }
        else
        {
            Debug.LogWarning("AudioClip ou AudioSource não está configurado.");
        }
    }

    // Corrotina para restaurar a flag e desativar a sirene após o término do áudio
    private IEnumerator ResetAudioFlagAfterPlay(float audioDuration)
    {
        yield return new WaitForSeconds(audioDuration);
        hasPlayedAudio = false; // Reseta a flag permitindo nova reprodução do som
        GameManager.sirene = false; // Desativa a sirene no GameManager
    }
}

