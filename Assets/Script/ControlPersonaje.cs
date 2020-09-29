using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    private float JumpForce = 30f;
    public GameObject Bullet;
    public GameObject BulletLeft;

    public AudioClip AudioJump;
    public AudioClip AudioAtaque;
    public AudioClip AudioCoins;

    public ControlPuntaje Puntaje;

    private AudioSource _audioSource;
    private Transform _transform;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private const int ANIM_QUIETO = 0;
    private const int ANIM_CAMINA = 1;
    private const int ANIM_SALTAR = 2;
    private const int ANIM_CORRER = 3;
    private const int ANIM_MUERTE = 4;

    private bool muerte = false;
    private bool coins = false;

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
            rb.velocity = new Vector2(5,rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CAMINA);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CAMINA);
            sr.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.RightShift))
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CORRER);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightShift))
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            animator.SetInteger("Estado", ANIM_CORRER);
            sr.flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.AddForce( new Vector2(rb.velocity.x, JumpForce),ForceMode2D.Impulse);
            animator.SetInteger("Estado", ANIM_SALTAR);
            _audioSource.PlayOneShot(AudioJump);
        }
        if (Input.GetKeyUp("x"))
        {
            if (!sr.flipX)
            {
                var BulletPosition = new Vector3(_transform.position.x + 3f, _transform.position.y, _transform.position.z);
                Instantiate(Bullet, BulletPosition, Quaternion.identity);
            }
            if (sr.flipX)
            {
                var BulletPosition = new Vector3(_transform.position.x - 3f, _transform.position.y, _transform.position.z);
                Instantiate(BulletLeft, BulletPosition, Quaternion.identity);
            }
            _audioSource.PlayOneShot(AudioAtaque);
        }
        if (muerte)
            animator.SetInteger("Estado", ANIM_MUERTE);
        if (coins)
        {
            _audioSource.PlayOneShot(AudioCoins);
            coins = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Enemigo")
        {
            muerte = true;
        }
        if (collision.gameObject.tag == "Coins")
        {
            coins = true;
            Destroy(collision.gameObject);
            _audioSource.PlayOneShot(AudioCoins);
            Puntaje.AddPoints(5);
            Debug.Log(Puntaje.GetPoint());
        }
    }
}
