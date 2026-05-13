using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 5f;

    void Start()
    {

    }

    public void OnMove(Vector2 input)
    {
        Vector2 move = new Vector2(0, input.y);

        move = transform.TransformDirection(move);
        rb2d.linearVelocityY = move.y * moveSpeed;
    }
}
