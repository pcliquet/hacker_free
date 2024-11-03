using UnityEngine;
using System.Collections;

public class ShadowGuardian : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float returnSpeed = 8f; // Velocidade de retorno
    public float detectionRange = 1f; // Distância de detecção
    public float attackRange = 2f; // Distância de ataque
    public int health = 100;
    public int shadowStrikeDamage = 20;
    public LayerMask solidObjectLayer; // Camada para verificar obstáculos
    public LayerMask playerLayer; // Camada do jogador para raycast

    private Transform player;
    private bool isChasing = false;
    private Vector2 initialPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position; // Armazena a posição inicial do inimigo
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // Verifica se a sirene está ativada
        if (GameManager.sirene)
        {
            if (distanceToPlayer < 7.5)
            {
                isChasing = true;
            }
        }
        else
        {
            // Se a sirene estiver desativada, volta à posição inicial
            isChasing = false;
            ReturnToInitialPosition();
        }

        if (isChasing)
        {
            GameManager.sirene = true;
            ChasePlayer();
            if (distanceToPlayer <= attackRange)
            {
                ShadowStrike();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // Lógica de patrulha - mover em uma área pré-definida
    }

    private void ChasePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        // Raycast para verificar se há um obstáculo entre o inimigo e o jogador
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, solidObjectLayer);

        if (hit.collider == null)
        {
            // Se não houver obstáculo, move em direção ao jogador
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            // Ajusta a direção se houver um obstáculo detectado
            Vector2 avoidDirection = Vector2.Perpendicular(directionToPlayer); // Direção perpendicular para desvio

            // Tenta mover em direção alternativa até passar do obstáculo
            Vector2 newTargetPos = (Vector2)transform.position + avoidDirection * 0.5f;
            
            if (isWalkable(newTargetPos))
            {
                transform.position = Vector2.MoveTowards(transform.position, newTargetPos, chaseSpeed * Time.deltaTime);
            }
        }
    }

    private bool isWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectLayer) == null; // Retorna verdadeiro se o espaço está livre
    }

    private void ReturnToInitialPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

        // Corrige a posição final se estiver perto
        if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            transform.position = initialPosition;
        }
    }

    private void ShadowStrike()
    {
        // Aplicar dano no jogador e efeito visual
       // Debug.Log("Ataque das Sombras!");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Guardião das Sombras derrotado!");
        Destroy(gameObject);
    }
}

