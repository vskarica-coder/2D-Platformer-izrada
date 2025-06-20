using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlienController : MonoBehaviour
{

    public enum CharacterState
    {
        Idle, Walk, Jump
    }

    private CharacterState characterState;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Checkpoint checkpoint;
    private Vector3 spawnPosition;



    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask deathLayer;

    [SerializeField] private BoxCollider2D boxCollider;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }



    private void Update()
    {
        if (ShoudDie())
        {
            if (checkpoint != null)
            {
                transform.position = checkpoint.transform.position;
            }
            else
            {
                transform.position = spawnPosition;
            }
            
            LivesManager.Instance?.LoseLife();
        }


        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (rb.velocity.x != 0)
        {
            spriteRenderer.flipX = rb.velocity.x < 0;
        }

        //checkpoint
        if (IsColiding(deathLayer))
        {
            transform.position = checkpoint.transform.position;
        }

        //Animacije
        bool isGrounded = IsGrounded();
        if (isGrounded && rb.velocity.x != 0)
        {
            characterState = CharacterState.Walk;
        }
        else if (!isGrounded)
        {
            characterState = CharacterState.Jump;
        }
        else
            characterState = CharacterState.Idle;
        animator.SetInteger("state", (int)characterState);
    }





        



        private bool IsColiding(LayerMask layerMask)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f, layerMask);
            return hit.collider != null;
        }



        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f, groundLayer);
            return hit.collider != null;
        }

        private bool ShoudDie()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f, deathLayer);
            return hit.collider != null;
        }


        //coin
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Coin"))
            {
                
                Destroy(collision.gameObject);
                CoinManager.Instance.AddCoin();
        }

            //checkpoint
        if (collision.CompareTag("Checkpoint"))
        {
            if (checkpoint != null)
                checkpoint.SetCheckpointActive(false);
            checkpoint = collision.GetComponent<Checkpoint>();
            checkpoint.SetCheckpointActive(true);
        }

    }

    public void Respawn()
    {
        if (checkpoint != null)
            transform.position = checkpoint.transform.position;
        else
            transform.position = spawnPosition;
    }



    private void OnDrawGizmos()
        {
            // Visualize the ground detection ray in the editor
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (boxCollider.bounds.extents.y + 0.1f));
        }
    }
