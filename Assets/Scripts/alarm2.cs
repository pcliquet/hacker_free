using UnityEngine;
using System.Collections;

public class AlarmCollisionAudio2 : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip collisionSound; // Áudio a ser reproduzido
    private AudioSource audioSource; // Componente AudioSource
    private bool hasPlayedAudio = false; // Flag para verificar se o áudio já foi tocado
    public bool isTriggerEnabled = true; // Flag para habilitar/desabilitar a função de trigger

    private SpriteRenderer spriteRenderer; // Referência ao componente SpriteRenderer
    private UnityEngine.Rendering.Universal.Light2D alarmLight; // Referência ao componente Light2D

    // Flags que controlam a ativação da Camera 2
    public bool button1Active = false; // Ativada quando o Botão 1 é pressionado
    public bool button2Active = true; // Ativada quando o Botão 2 é pressionado
    public bool button3Active = false; // Ativada quando o Botão 3 é pressionado

    private void Awake()
    {
        button1Active = false; // Estado inicial do Botão 1
        button2Active = true; // Estado inicial do Botão 2
        button3Active = false; // Estado inicial do Botão 3
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
        // Imprime o estado das flags dos botões
       // Debug.Log($"Button1 Active: {button1Active}, Button2 Active: {button2Active}, Button3 Active: {button3Active}");

        // Verifica se todas as flags estão ativas
        if (button1Active && !button2Active && button3Active)
        {
            // Desativa a imagem e a colisão
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false; // Desativa a visibilidade da sprite
            }

            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false; // Desativa a colisão
            }

            if (alarmLight != null)
            {
                alarmLight.enabled = false; // Desativa a luz
            }

            isTriggerEnabled = false; // Desativa o trigger para não permitir mais colisões
        }
        // Verifica se o Botão 2 está ativo e os Botões 1 e 3 estão inativos
        else if (!button1Active && button2Active && !button3Active)
        {
            // Ativa a imagem e a colisão
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true; // Ativa a visibilidade da sprite
            }

            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = true; // Ativa a colisão
            }

            if (alarmLight != null)
            {
                alarmLight.enabled = true; // Ativa a luz
            }

            isTriggerEnabled = true; // Reativa o trigger
        }
    }

    public void Button1Pressed()
    {
        button1Active = true; // Ativa a flag do Botão 1
    }        

    public void Button2Pressed()
    {
        button1Active = false; // Desativa a flag do Botão 1
        button2Active = true; // Ativa a flag do Botão 2
        button3Active = false; // Desativa a flag do Botão 3
    }     

    public void Button3Pressed()
    {
        button3Active = true; // Ativa a flag do Botão 3
        button2Active = false; // Desativa a flag do Botão 2
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggerEnabled)
        {
            return; // Se a flag estiver desativada, não faz nada
        }

        if (other.CompareTag("Player") && !hasPlayedAudio)
        {
            PlayCollisionAudio(); // Toca o áudio
        }
    }

    private void PlayCollisionAudio()
    {
        if (collisionSound != null && audioSource != null)
        {
            hasPlayedAudio = true;
            audioSource.PlayOneShot(collisionSound); // Reproduz o áudio
            GameManager.sirene = true; // Ativa a sirene
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
        GameManager.sirene = false; // Desativa a sirene quando o áudio termina
    }
}

