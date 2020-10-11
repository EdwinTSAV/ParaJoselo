using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGoku : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private const float JumpForce = 20f;
    private const int ANI_VOLAR = 1;
    private const int ANI_CORRE = 0;

    private bool volar = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0,rb.velocity.y);

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) && volar)
        {
            rb.velocity = new Vector2(0,0);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(3, rb.velocity.y);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-3, rb.velocity.y);
            sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && rb.gravityScale == 10)
        {
            rb.AddForce(new Vector2(rb.velocity.x, JumpForce), ForceMode2D.Impulse);
        }
        if (volar)
        {
            animator.SetInteger("Estado", ANI_VOLAR);
            rb.gravityScale = 0;
        }

        if (Input.GetKey("x"))
        {
            animator.SetInteger("Estado", ANI_CORRE);
            rb.gravityScale = 10;
            volar = false;
        }

        if (Input.GetKey(KeyCode.UpArrow) && volar)
        {
            rb.velocity = new Vector2( rb.velocity.x, 3);
        }
        if (Input.GetKey(KeyCode.DownArrow) && volar)
        {
            rb.velocity = new Vector2( rb.velocity.x, -3);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PaVolar")
        {
            volar = true;
        }
    }
}
