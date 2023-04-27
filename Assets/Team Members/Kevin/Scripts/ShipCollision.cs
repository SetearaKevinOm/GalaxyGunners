using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipCollision : MonoBehaviour
{
    public GameManager instance;
    public Action shipTakeDamage;
    public GameObject windowPrefab;
    public ParticleSystem cockpitParticles;
    
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
            //Instantiate(cockpitParticles, windowPrefab.GetComponent<WindowCollision>().shieldCollisionTransforms[Random.Range(0,4)].position, Quaternion.identity);
            instance.shipHealth -= enemyBase.enemyDamage;
            instance.uiManager.uiShipRef.GetComponent<ShipDamageUI>().ShipDamaged();
            shipTakeDamage.Invoke();
            col.gameObject.SetActive(false);
            col.gameObject.transform.position = instance.rubbishBinScript.transform.position;
        }
    }

    
}
