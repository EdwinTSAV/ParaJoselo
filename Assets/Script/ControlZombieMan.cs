using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZombieMan : MonoBehaviour
{
    private Rigidbody2D rb;
    private float velocidad = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(velocidad * -1, rb.velocity.y);
    }
}
