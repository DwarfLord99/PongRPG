using UnityEngine;

public class PongBall : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private float speed = 5f;

    //private float yDirection = 0.0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = Mathf.Abs(RandomStartSpeed());

        float horizontalDirection = RandomStartSpeed() > 0 ? 1f : -1f;
        float verticalDirection = Random.Range(-0.5f, 0.5f); // vertical offset

        Vector2 launchDir = new Vector2(horizontalDirection, verticalDirection).normalized;
        rb2d.linearVelocity = launchDir * speed;
    }

    float RandomStartSpeed()
    {
        return Random.Range(0, 2) == 0 ? speed : -speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Increase ball speed
            if (Mathf.Abs(speed) < 25.0f)
            {
                speed += speed > 0 ? 0.5f : -0.5f; // Maintain direction while increasing speed
            }

            speed = -speed; // reverse the speed to change direction

            // Update the ball's velocity
            rb2d.linearVelocity = new Vector2(speed, rb2d.linearVelocity.y);

            ChangeYDirection(collision);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x,
                Mathf.Sign(rb2d.linearVelocity.y) * (rb2d.linearVelocity.magnitude / 2.0f));
        }
    }

    void ChangeYDirection(Collision2D collision)
    {
        // Calculate the hit point relative to the paddle's center
        Vector3 hitPoint = collision.GetContact(0).point;
        Vector3 paddleCenter = collision.collider.bounds.center;
        // Determine the vertical offset from the paddle's center
        float offset = hitPoint.y - paddleCenter.y;
        // Normalize the offset to a range of -1 to 1
        float normalizedOffset = offset / (collision.collider.bounds.size.y / 2);
        // Adjust the ball's vertical velocity based on the normalized offset
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, normalizedOffset * speed);
    }
}
