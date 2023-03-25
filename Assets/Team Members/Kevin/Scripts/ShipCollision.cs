using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        EnemyBase enemyBase = col.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            GameManager.Instance.shipHealth -= enemyBase.enemyDamage;
            Destroy(col.gameObject);
            Debug.Log("Ship has been hit: " + enemyBase.enemyDamage);
        }
    }
}
