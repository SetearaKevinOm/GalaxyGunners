using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyProjectile : EnemyBase
{
    private Rigidbody _rb;
    public Transform balisticsTransform;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        enemyDamage = 50;
        projectileSpeed = Random.Range(7500f,8000f);
        if (_rb == null) return;
        _rb.AddForce(balisticsTransform.transform.forward * projectileSpeed,ForceMode.Force);
    }
}