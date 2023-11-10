using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed = 5;

    Vector2 movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");


        movement.y = Input.GetAxisRaw("Vertical");

        bool W = Input.GetKey(KeyCode.W);
        bool A = Input.GetKey(KeyCode.A);
        bool S = Input.GetKey(KeyCode.S);
        bool D = Input.GetKey(KeyCode.D);
        Vector2 pos = new Vector2(0,0);
        
        if (W)
        {
            pos.y += 1;
        }

        if (A)
        { 
            pos.x += -1; 
        }

        if (S)
        { 
            pos.y += -1; 
        }

        if (D)
        { 
            pos.x += 1;
        }
        pos = pos.normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
