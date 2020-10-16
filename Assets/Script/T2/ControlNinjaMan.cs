using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNinjaMan : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform _transform;

    private const int ANIM_QUIETO = 0;
    private const int ANIM_CORRER = 1;
    private const int ANIM_SALTAR = 2;
    private const int ANIM_ATACAR = 3;
    private const int ANIM_AGACHA = 4;
    private const int ANIM_TREPAR = 5;
    private const int ANIM_VOLAR = 6;
    private const int ANIM_MUERTE = 7;

    private float velocity = 10f;
    private float JumpForce = 30f;
    private int numSalto = 0;
    private int vidas = 3;

    public GameObject KunaiRigth;
    public GameObject KunaiLeft;

    public VidaText Vida;
    public ControlPuntaje Puntaje;


    private bool trepar = false;
    private bool muerte = false;
    private bool planear = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetInteger("Estado", ANIM_QUIETO);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CORRER);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CORRER);
            sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && numSalto < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x, JumpForce), ForceMode2D.Impulse);
            animator.SetInteger("Estado", ANIM_SALTAR);
            numSalto++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetInteger("Estado", ANIM_AGACHA);
        }
        if (trepar)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            animator.SetInteger("Estado", ANIM_TREPAR);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, velocity);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, -velocity);
            }
        }
        if (!trepar)
            rb.gravityScale = 10;

        if (Input.GetKeyUp("x"))
        {
            animator.SetInteger("Estado", ANIM_ATACAR);
            if (!sr.flipX)
            {
                var KunaiPosition = new Vector3(_transform.position.x + 3f, _transform.position.y, _transform.position.z);
                Instantiate(KunaiRigth, KunaiPosition, Quaternion.identity);
            }
            if (sr.flipX)
            {
                var KunaiPosition = new Vector3(_transform.position.x - 3f, _transform.position.y, _transform.position.z);
                Instantiate(KunaiLeft, KunaiPosition, Quaternion.identity);
            }
        }

        if (muerte)
            animator.SetInteger("Estado", ANIM_MUERTE);

        if (Input.GetKey("c") && planear)
        {
            rb.gravityScale = 1;
            numSalto = 2;
            animator.SetInteger("Estado", ANIM_VOLAR);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(velocity, -velocity);
                sr.flipX = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-velocity, -velocity);
                sr.flipX = true;
            }
        }

        Caida();
    }

    private void Caida()
    {
        if (rb.velocity.y < -65)
        {
            muerte = true;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            vidas--;
            if (vidas == 0) muerte = true;
            if (vidas >= 0)
            {
                Vida.QuitarVida(1);
                Debug.Log(Vida.GetVida());
            }

        }
        if (collision.gameObject.layer == 8)
        {
            numSalto = 0;
            //planear = false;

        }
        if (collision.gameObject.layer == 8)
        {
            numSalto = 0;
            planear = false;

        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            planear = true;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Escalera")
        {
            trepar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Escalera")
        {
            trepar = false;
        }
    }
}
