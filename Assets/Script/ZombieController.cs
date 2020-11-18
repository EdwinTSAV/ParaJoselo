using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // Start is called before the first frame update
   private Rigidbody2D rb;
   private SpriteRenderer sr;
   private Animator animator;
   private BoxCollider2D bx;
   private bool EstadoChoque = false;
   
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bx = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EstadoChoque == false)
        {
            sr.flipX = false;
            rb.velocity = new Vector2(1, rb.velocity.y);
        
        }
        if (EstadoChoque)
        {
            sr.flipX = true;
            rb.velocity = new Vector2(-1,rb.velocity.y);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag=="Caja")
        {
            EstadoChoque = true;
        }
        if (collision.gameObject.tag=="CajaIzq")
        {
            EstadoChoque = false;
        }
    }
}
