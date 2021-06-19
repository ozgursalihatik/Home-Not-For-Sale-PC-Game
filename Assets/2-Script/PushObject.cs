using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private int _speed;

    private Rigidbody _rg;
    
    
    void Start()
    {
        _rg = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Dokundu");
        }
    }
}
