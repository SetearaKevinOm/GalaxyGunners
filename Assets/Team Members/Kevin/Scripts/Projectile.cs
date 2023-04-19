using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

/*public enum ProjectileColor
{
    Red,
    Blue
}*/
public class Projectile : ColorEnum
{
    private Rigidbody _rb;
    public float projectileSpeed;
    public Transform balisticsTransform;
    public int projectileDmg;
    public MyColor myColor;
   
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
        EnemyBase enemyParent = other.GetComponentInParent<EnemyBase>();
        if (GameManager.Instance.isColorSchemed)
        {
            if (enemy != null && myColor == enemy.myColor)
            {
                enemy.OnClicked(projectileDmg, transform);
                Destroy(gameObject);
                //StartCoroutine(ProjectileHitLife());
            }
            else if (enemyParent != null && myColor == enemy.myColor)
            {
                enemyParent.OnClicked(projectileDmg, transform);
                Destroy(gameObject);
            }
        }
        else if(!GameManager.Instance.isColorSchemed)
        {
            if (enemy != null)
            {
                enemy.OnClicked(projectileDmg, transform);
                Destroy(gameObject);
                //StartCoroutine(ProjectileHitLife());
            }
            else if (enemyParent != null)
            {
                enemyParent.OnClicked(projectileDmg, transform);
                Destroy(gameObject);
            }
        }
        
        
    }

    private IEnumerator ProjectileHitLife()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
