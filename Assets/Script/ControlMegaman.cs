using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMegaman : MonoBehaviour
{
    public GameObject BalaCalibre1;
    public GameObject BalaCalibre2;
    public GameObject BalaCalibre3;
    public GameObject BalaCalibre4;
    public GameObject BalaSuperCalibre5;

    public GameObject BalaCalibre1L;
    public GameObject BalaCalibre2L;
    public GameObject BalaCalibre3L;
    public GameObject BalaCalibre4L;
    public GameObject BalaSuperCalibre5L;

    private const float Bala1 = 1;
    private const float Bala2 = 3;
    private const float Bala3 = 5;
    private const float Bala4 = 7;
    private const float Bala5 = 9;
    private float tiempo = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform _transform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            sr.flipX = true;
        }
        if (Input.GetKey("x"))
        {
            tiempo += Time.deltaTime;
            Debug.Log("El tiempo es de : " + tiempo);
        }
        if (Input.GetKeyUp("x"))
        {
            Debug.Log("El tiempo es de : " + tiempo);
            if (tiempo >= Bala1 && tiempo <= Bala2)
            {
                if (!sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x + 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre1, BulletPosition, Quaternion.identity);
                }
                if (sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x - 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre1L, BulletPosition, Quaternion.identity);
                }
            }
            if (tiempo >= Bala2 && tiempo <= Bala3)
            {
                if (!sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x + 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre2, BulletPosition, Quaternion.identity);
                }
                if (sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x - 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre2L, BulletPosition, Quaternion.identity);
                }
            }
            if (tiempo >= Bala3 && tiempo <= Bala4)
            {
                if (!sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x + 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre3, BulletPosition, Quaternion.identity);
                }
                if (sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x - 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre3L, BulletPosition, Quaternion.identity);
                }
            }
            if (tiempo >= Bala4 && tiempo <= Bala5)
            {
                if (!sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x + 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre4, BulletPosition, Quaternion.identity);
                }
                if (sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x - 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaCalibre4L, BulletPosition, Quaternion.identity);
                }
            }
            if (tiempo >= Bala5)
            {
                if (!sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x + 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaSuperCalibre5, BulletPosition, Quaternion.identity);
                }
                if (sr.flipX)
                {
                    var BulletPosition = new Vector3(_transform.position.x - 1f, _transform.position.y, _transform.position.z);
                    Instantiate(BalaSuperCalibre5L, BulletPosition, Quaternion.identity);
                }
            }
            tiempo = 1;
        }

    }
}
