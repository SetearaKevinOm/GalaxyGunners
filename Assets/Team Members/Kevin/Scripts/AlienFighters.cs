using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class AlienFighters : EnemyBase
{
    public bool canMove;
    public void Update()
    {
        if (canMove)
        {
            gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * 100f);
            transform.LookAt(GameManager.Instance.enemyThreshold.transform.position);
            transform.position += transform.forward * projectileSpeed * Time.deltaTime;
        }
    }
    
}
