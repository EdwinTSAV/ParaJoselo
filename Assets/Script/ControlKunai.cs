using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlKunai : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D rb;

    private ControlPuntaje Puntaje;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Puntaje = FindObjectOfType<ControlPuntaje>();
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
            Puntaje.AddPoints(10);
            Debug.Log(Puntaje.GetPoint());
        }
    }
}
