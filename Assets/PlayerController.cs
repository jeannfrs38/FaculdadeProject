using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float eixoX;
    public float eixoY;
    public Rigidbody2D rb;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        eixoX = Input.GetAxisRaw("Horizontal");
        eixoY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(eixoX, eixoY) * speed;
    }
}
