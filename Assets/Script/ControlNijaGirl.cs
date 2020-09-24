using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNijaGirl : MonoBehaviour
{
    private float velocity = 10f;
    private float JumpForce = 30f;

    public AudioClip AudioJump;
    public AudioClip AudioAtaque;

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

    private bool muerte = false;
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
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(rb.velocity.x, JumpForce), ForceMode2D.Impulse);
            animator.SetInteger("Estado", ANIM_SALTAR);
            _audioSource.PlayOneShot(AudioJump);
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
        if (muerte)
            animator.SetInteger("Estado", ANIM_MUERTE);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Enemigo")
        {
            muerte = true;
        }
    }
}
