using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform minPos;
    [SerializeField] Transform maxPos;

    private Rigidbody2D rb2d;
    private float reactionTimer = 0.0f; // Timer to control reaction delay

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball == null) return;

        reactionTimer += Time.deltaTime;
        if (reactionTimer < 0.2f) return; // Enemy reacts every 0.2 seconds

        // Calculate the direction to the ball
        Vector2 ballDirection = (ball.transform.position - transform.position).normalized;

        rb2d.linearVelocity = new Vector2(0, ballDirection.y * 5f);

        // Clamp the enemy's position within the defined bounds
        float clampedY = Mathf.Clamp(transform.position.y, minPos.position.y, maxPos.position.y);
        transform.position = new Vector2(transform.position.x, clampedY);
    }
}
