using UnityEngine;

public class Boss : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float attackRange = 2f; // Distância de ataque
    public int health = 100;
    public int shadowStrikeDamage = 20;
    public LayerMask solidObjectLayer; // Camada para verificar obstáculos

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        ChasePlayer();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            ShadowStrike();
        }
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
        // Debug.Log("Guardião das Sombras derrotado!");
        Destroy(gameObject);
    }
}

