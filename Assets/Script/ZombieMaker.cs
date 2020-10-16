using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMaker : MonoBehaviour
{
    public GameObject Zombie;
    private Transform _transform;
    private float tiempo = 0f;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {

        tiempo += Time.deltaTime;
        if (tiempo >= 5)
        {
            Instantiate(Zombie, _transform.position, Quaternion.identity);
            tiempo = 0;
        }
    }
}
