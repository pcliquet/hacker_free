using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    public LayerMask solidObjectLayer;
    public GameObject interactionCanvas; 
    private bool isInteracting = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isInteracting)
        {
            HandleMovement();
        }

        // Interação com a tecla Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isInteracting)
            {
                StartInteraction();
            }
            else
            {
                EndInteraction();
            }
        }
    }

    private void HandleMovement()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("movX", input.x);
                animator.SetFloat("movY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true; 
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; 
        }
        isMoving = false; 
    }

    private bool isWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectLayer) == null;
    }

    private void StartInteraction()
    {
        isInteracting = true; 
        interactionCanvas.SetActive(true); 
        Time.timeScale = 0f; 
    }

    private void EndInteraction()
    {
        isInteracting = false; 
        interactionCanvas.SetActive(false); 
        Time.timeScale = 1f; 
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
       // Verifica se colidiu com a polícia
        if (other.CompareTag("Policia"))
        {
            Debug.Log("Colidiu com a polícia! Mudando para a cena Busted."); // Debug para verificar colisão
            SceneManager.LoadScene("Busted"); // Altera para a cena "Busted"
        }
    }


}

