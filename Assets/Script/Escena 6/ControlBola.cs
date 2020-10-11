using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBola : MonoBehaviour
{
    private Rigidbody2D rb;
    private float velX = 5;
    private float velY = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX,velY) ;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ParedLateral")
            velX = velX * -1;
        if (collision.gameObject.tag == "ParedSuperior")
            velY = velY * -1;
    }
}
