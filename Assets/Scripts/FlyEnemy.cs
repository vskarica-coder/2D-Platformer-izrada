using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 startPoint;
    private bool movingRight = true;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void Update()
    {
        float move = moveSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(Vector2.right * move);

        if (Vector2.Distance(transform.position, startPoint) >= moveDistance)
        {
            movingRight = !movingRight;
            Flip();
            startPoint = transform.position;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 center = collision.collider.bounds.center;

            // Ako je igrač iznad
            if (contactPoint.y > center.y + 0.1f)
            {
                Die();
            }
            else
            {
                // Igrač gubi život
                LivesManager.Instance?.LoseLife();
                collision.collider.GetComponent<AlienController>().Respawn(); // Ako koristiš respawn logiku
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
