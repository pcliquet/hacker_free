using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveDistanceX = 5f; // Distância para mover no eixo X
    public float moveDistanceY = 0f; // Distância para mover no eixo Y
    public float moveSpeed = 2f;     // Velocidade de movimento

    private Vector3 startPosition;   // Posição inicial do objeto
    private bool movingToEnd = true; // Controle da direção do movimento

    private void Start()
    {
        startPosition = transform.position; // Armazena a posição inicial do objeto
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Define a posição de destino
        Vector3 targetPosition = movingToEnd 
            ? startPosition + new Vector3(moveDistanceX, moveDistanceY, 0) 
            : startPosition;

        // Move o objeto na direção do destino
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Inverte a direção ao alcançar o destino
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingToEnd = !movingToEnd;
        }
    }
}

