using UnityEngine;
using System.Collections;

public class AlarmCollisionAudio3 : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip collisionSound; // Áudio a ser reproduzido
    private AudioSource audioSource; // Componente AudioSource
    private bool hasPlayedAudio = false; // Flag para verificar se o áudio já foi tocado
    public bool isTriggerEnabled = true; // Flag para habilitar/desabilitar a função de trigger

    private SpriteRenderer spriteRenderer; // Referência ao componente SpriteRenderer
    private UnityEngine.Rendering.Universal.Light2D alarmLight; // Referência ao componente Light2D

    // Flags que controlam a ativação da Camera 3
    public bool button4Active = false; // Ativada quando o Botão 4 é pressionado
    public bool button5Active = false; // Ativada quando o Botão 5 é pressionado

    private void Awake()
    {
        button4Active = false; // Estado inicial do Botão 4
        button5Active = false; // Estado inicial do Botão 5
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
        // Verifica se o Botão 4 está ativo
        if (button4Active)
        {
            // Lógica para desativar o alarme
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

        // Verifica se o Botão 5 está ativo
        if (button5Active)
        {
            // Lógica para desativar o alarme de acordo com a necessidade
            // Aqui você pode adicionar a lógica específica para o Botão 5
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

    public void DeactivateCamera()
    {
        button4Active = true; // Chama o método para desativar a câmera
    }

    public void ActivateCamera()
    {
        button4Active = false; // Chama o método para ativar a câmera novamente
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // Reativa a visibilidade da sprite
        }

        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = true; // Reativa a colisão
        }

        if (alarmLight != null)
        {
            alarmLight.enabled = true; // Reativa a luz
        }
        isTriggerEnabled = true; // Reativa o trigger
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
            // Ative a sirene ou outro efeito aqui
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

