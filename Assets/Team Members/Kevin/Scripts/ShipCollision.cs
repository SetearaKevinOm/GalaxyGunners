using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    public GameManager instance;
    public float shakeDuration;
    public float shakeMagnitude;

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
            instance.shipHealth -= enemyBase.enemyDamage;
            shipTakeDamage.Invoke();
            //StartCoroutine(instance.cameraShake.Shake(shakeDuration, shakeMagnitude));
            col.gameObject.SetActive(false);
            col.gameObject.transform.position = instance.rubbishBinScript.transform.position;
            Debug.Log("Ship has been hit: " + enemyBase.enemyDamage);
        }
    }

    
}
