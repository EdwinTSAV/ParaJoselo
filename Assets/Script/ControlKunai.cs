using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlKunai : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 3);
    }

    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
