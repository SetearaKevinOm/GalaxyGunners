using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : EnemyBase
{
    public Transform targetLocation;
    public Transform newRTargetLocation;
    public Transform newLTargetLocation;
    public bool singleUse = true;
    public void Start()
    {
        projectileSpeed = Random.Range(5f, 10f);
        targetLocation = instance.shipCollisionBox.transform;
        //newRTargetLocation = instance.targetTransformR.transform;
        //newRTargetLocation = instance.targetTransformL.transform;
        StartCoroutine(Life());
    }

    public void Update()
    {
        //gameObject.transform.Rotate(new Vector3(randomX,randomY,randomZ),Space.Self);
        //transform.eulerAngles = Vector3.forward * projectileSpeed * Time.deltaTime;
        //if (instance.asteroidPhaseEnd && singleUse) targetLocation = NewTarget();
        transform.LookAt(targetLocation.position);
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private Transform NewTarget()
    {
        int coinFlip = Random.Range(0, 100);
        if (coinFlip <= 49)
        {
            targetLocation = newLTargetLocation;
        }
        else if(coinFlip > 50)
        {
            targetLocation = newRTargetLocation;
        }

        singleUse = false;
        return targetLocation;
    }

    private IEnumerator Life()
    {
        yield return new WaitForSeconds(30f);
        GameManager.Instance.asteroidsSpawned.Remove(gameObject);
        Destroy(gameObject);
    }
}
