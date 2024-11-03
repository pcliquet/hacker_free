using UnityEngine;
using System.Collections;

public class AlarmCollisionAudio4 : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip collisionSound; // Áudio a ser reproduzido
    private AudioSource audioSource; // Componente AudioSource
    private bool hasPlayedAudio = false; // Flag para verificar se o áudio já foi tocado
    public bool isTriggerEnabled = true; // Flag para habilitar/desabilitar a função de trigger

    private SpriteRenderer spriteRenderer; // Referência ao componente SpriteRenderer
    private UnityEngine.Rendering.Universal.Light2D alarmLight; // Referência ao componente Light2D

    // Flags que controlam a ativação dos botões
    public bool button4Active = false; // Ativada quando o Botão 4 é pressionado
    public bool button5Active = false; // Ativada quando o Botão 5 é pressionado
    public bool button2Active = false; // Ativada quando o Botão 2 é pressionado

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        alarmLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    private void Update()
    {
        // Controla a visibilidade da sprite e a ativação da luz com base nas flags dos botões
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = button4Active && button5Active && button2Active; 
        }

        if (alarmLight != null)
        {
            alarmLight.enabled = button4Active && button5Active && button2Active; 
        }

        // Se qualquer botão estiver inativo, reativa o trigger
        if (!button4Active || !button5Active || !button2Active)
        {
            isTriggerEnabled = true;
            hasPlayedAudio = false; // Permite que o áudio possa tocar novamente
        }
    }

    public void Button4Pressed()
    {
        button4Active = true; // Ativa a flag do Botão 4
    }

    public void Button5Pressed()
    {
        button5Active = true; // Ativa a flag do Botão 5
    }

    public void Button2Pressed() // Método para ativar a flag do Botão 2
    {
        button2Active = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o trigger está habilitado e se todos os botões estão ativos
        if (!isTriggerEnabled || !button4Active || !button5Active || !button2Active)
        {
            return; // Se qualquer flag estiver desativada, não faz nada
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
            hasPlayedAudio = true; // Define a flag como true para evitar que o som seja tocado repetidamente
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

    private IEnumerator ResetAudioFlagAfterPlay(float audioDuration)
    {
        yield return new WaitForSeconds(audioDuration); // Espera o tempo do áudio
        hasPlayedAudio = false; // Reseta a flag, permitindo que o som possa tocar novamente
        GameManager.sirene = false; // Desativa a sirene após o áudio terminar
    }
}

