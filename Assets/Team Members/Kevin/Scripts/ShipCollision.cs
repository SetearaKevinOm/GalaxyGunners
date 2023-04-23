using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    public GameManager instance;
    public Action shipTakeDamage;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        instance = GameManager.Instance;
    }

    public void OnTriggerEnter(Collider col)
    {
        EnemyBase enemyBase = col.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            if (col.GetComponent<Asteroid>() != null)
            {
                instance.AsteroidDestroyed(col.gameObject);
            }

            if (col.GetComponentInParent<AlienFighters>() != null)
            {
                Destroy(col.GetComponentInParent<AlienFighters>().gameObject);
                instance.alienFighterSpawner.alienFighters.Remove(col.GetComponentInParent<AlienFighters>().gameObject);
            }
            instance.shipHealth -= enemyBase.enemyDamage;
            instance.uiManager.uiShipRef.GetComponent<ShipDamageUI>().ShipDamaged();
            shipTakeDamage.Invoke();
            col.gameObject.SetActive(false);
            col.gameObject.transform.position = instance.rubbishBinScript.transform.position;
        }
    }

    
}
