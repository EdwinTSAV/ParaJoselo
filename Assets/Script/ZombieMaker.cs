using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMaker : MonoBehaviour
{
    public GameObject Zombie;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
        Instantiate(Zombie, _transform.position,Quaternion.identity);
    }

    void Update()
    {
        
    }
}
