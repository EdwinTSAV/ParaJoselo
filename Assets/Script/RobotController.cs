using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb; //Pisicion
    private Animator animator; //Movimiento
    private SpriteRenderer sr;
    private bool vivo = true;
    void Start()
    {  
        //Aumentar disminuir la velocidad del objeto
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vivo)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            animator.SetInteger("Estado",0); //Siempre vuelve a este valor
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(10,rb.velocity.y);
                animator.SetInteger("Estado",1); //Seteo un valor para la animación
                sr.flipX = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-10,rb.velocity.y);
                animator.SetInteger("Estado",1); //Seteo un valor para la animación
                sr.flipX = true;
            
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //rb.velocity = new Vector2(0,40);
                rb.AddForce(new Vector2(0,1),ForceMode2D.Impulse);
                animator.SetInteger("Estado",1); //Seteo un valor para la animación
            }

            
        }
        else
        {
            animator.SetInteger("Estado",4);
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag=="Zombie")
        {
            vivo = false;
        }
    }

}
