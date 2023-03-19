using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class AlienFighters : EnemyBase
{
    public void Start()
    {
        health = 100f;
        projectileSpeed = 25f;
    }

    public void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * 100f);
        transform.LookAt(GameManager.Instance.shipCollisionBox.transform.position);
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
    
}
