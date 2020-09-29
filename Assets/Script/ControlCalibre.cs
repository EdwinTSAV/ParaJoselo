using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCalibre : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
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
