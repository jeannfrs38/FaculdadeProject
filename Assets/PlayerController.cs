using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float eixoX;
    public float eixoY;
    public Rigidbody2D rb;

    public Animator animator;
    public bool isWalking = false;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        eixoX = Input.GetAxisRaw("Horizontal");
        eixoY = Input.GetAxisRaw("Vertical");
        isWalking = eixoX != 0 || eixoY != 0;

        rb.velocity = new Vector2(eixoX, eixoY) * speed;

        if (isWalking)
        {
            animator.SetFloat("eixoX", eixoX);
            animator.SetFloat("eixoY", eixoY);
        }

        animator.SetBool("isWalking", isWalking);
    }
}
