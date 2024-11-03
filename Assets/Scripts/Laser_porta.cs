using UnityEngine;
using System.Collections;

public class Laser_porta : MonoBehaviour
{
    public float activeTime = 5f;   // Tempo em segundos para o laser ficar ativo
    public float inactiveTime = 5f; // Tempo em segundos para o laser ficar inativo

    private Renderer laserRenderer; // Referência ao componente visual do laser
    private UnityEngine.Rendering.Universal.Light2D laserLight;     // Referência ao Light2D do laser
    private bool isLaserActive = true; // Flag para controle do estado do laser

    private void Start()
    {
        laserRenderer = GetComponent<Renderer>();
        laserLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>(); // Obtém o componente Light2D no mesmo objeto

        StartCoroutine(ToggleLaser());
    }

    private IEnumerator ToggleLaser()
    {
        while (isLaserActive) // Verifica se o laser ainda está ativo
        {
            // Ativa o laser e a luz
            laserRenderer.enabled = true;
            if (laserLight != null) laserLight.enabled = true;
            yield return new WaitForSeconds(activeTime); // Espera o tempo ativo

            // Desativa o laser e a luz
            laserRenderer.enabled = false;
            if (laserLight != null) laserLight.enabled = false;
            yield return new WaitForSeconds(inactiveTime); // Espera o tempo inativo
        }
    }
        private void Update()
        {
            // Verifica se a senha está correta
            if (GameManager.isPasswordCorrect)
            {
                DeactivateLaser(); // Desativa o laser se a senha estiver correta
            }
        }

    public void DeactivateLaser() // Função para desativar o laser
    {
        isLaserActive = false; // Desativa o laser
        laserRenderer.enabled = false; // Desativa a renderização
        if (laserLight != null) laserLight.enabled = false; // Desativa a luz
        Debug.Log("Laser desativado!"); // Log para verificar
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o laser colidiu com o player
        if (other.CompareTag("Player"))
        {
            Debug.Log("O laser tocou no player!");
            GameManager.sirene = true;

            // Aqui você pode adicionar mais lógica, como causar dano ao jogador
        }
    }
}

