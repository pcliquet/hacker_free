using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
    public float activeTime = 5f;   // Tempo em segundos para o laser ficar ativo
    public float inactiveTime = 5f; // Tempo em segundos para o laser ficar inativo
    public AudioSource laserAudio;  // Referência ao componente de áudio do laser

    private Renderer laserRenderer; // Referência ao componente visual do laser
    private UnityEngine.Rendering.Universal.Light2D laserLight; // Referência ao Light2D do laser
    private Collider2D laserCollider; // Referência ao Collider2D do laser

    private void Start()
    {
        laserRenderer = GetComponent<Renderer>();
        laserLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>(); // Obtém o componente Light2D no mesmo objeto
        laserCollider = GetComponent<Collider2D>(); // Obtém o componente Collider2D no mesmo objeto

        StartCoroutine(ToggleLaser());
    }

    private IEnumerator ToggleLaser()
    {
        while (true)
        {
            // Ativa o laser, a luz e o colisor
            laserRenderer.enabled = true;
            if (laserLight != null) laserLight.enabled = true;
            if (laserCollider != null) laserCollider.enabled = true;
            yield return new WaitForSeconds(activeTime); // Espera o tempo ativo

            // Desativa o laser, a luz e o colisor
            laserRenderer.enabled = false;
            if (laserLight != null) laserLight.enabled = false;
            if (laserCollider != null) laserCollider.enabled = false;
            yield return new WaitForSeconds(inactiveTime); // Espera o tempo inativo
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o laser colidiu com o player
        if (other.CompareTag("Player"))
        {
            GameManager.sirene = true;

            // Toca o áudio ao tocar no laser
            if (laserAudio != null && !laserAudio.isPlaying)
            {
                laserAudio.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o player saiu da área do laser

    }
}

