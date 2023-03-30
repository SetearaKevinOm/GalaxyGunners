using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    public float projectileSpeed;
    public Transform balisticsTransform;
    public int projectileDmg;
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        
    }
    
    private void Start()
    {
        _rb.AddForce(balisticsTransform.transform.forward * projectileSpeed,ForceMode.Force);
        StartCoroutine(ProjectileLife());
    }

    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.OnClicked(projectileDmg);
        }
    }
}
