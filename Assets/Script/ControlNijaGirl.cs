using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ControlNijaGirl : MonoBehaviour
{
    private float velocity = 10f;
    private float JumpForce = 30f;

    public AudioClip AudioJump;
    public AudioClip AudioAtaque;

    public VidaText Vida;

    private AudioSource _audioSource;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform _transform;
    public GameObject KunaiRigth;
    public GameObject KunaiLeft;

    private const int ANIM_QUIETO = 0;
    private const int ANIM_CORRER = 1;
    private const int ANIM_SALTAR = 2;
    private const int ANIM_ATACAR = 3;
    private const int ANIM_MUERTE = 4;
    private const int ANIM_AGACHA = 5;
    private const int ANIM_TREPAR = 6;
    private const int ANIM_VOLAR = 7;
    private const int ANIM_ATACAE = 8;



    private int vidas = 3;

    private bool muerte = false;
    private bool trepar = false;
    private bool planear = false;
    private int numSalto = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.PlayOneShot(AudioJump);
            numSalto++;
        }
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
            _audioSource.PlayOneShot(AudioAtaque);
        }

        if (Input.GetKeyDown("f"))
            animator.SetInteger("Estado", ANIM_ATACAE);

        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetInteger("Estado", ANIM_AGACHA);
        }
        if (muerte)
            animator.SetInteger("Estado", ANIM_MUERTE);

        if(trepar)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x,0) ;
            animator.SetInteger("Estado", ANIM_TREPAR);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, velocity);
                //animator.SetInteger("Estado", ANIM_TREPAR);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, -velocity);
                //animator.SetInteger("Estado", ANIM_TREPAR);
            }
        }
        if(!trepar)
            rb.gravityScale = 10;

        if (Input.GetKey("z") && planear)
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Enemigo")
        {
            muerte = true;
        }
        if (collision.gameObject.tag == "Pelota")
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
            planear = false;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Plataf")
        {
            planear = true;
            
            Debug.Log("A planear!!!!");
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Escalera")
    //    {
    //        Debug.Log("Colision con escalera");
    //    }
    //}
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
