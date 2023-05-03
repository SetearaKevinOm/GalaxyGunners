using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : EnemyBase
{
    public Transform targetLocation;
    private Transform _myTransform;
    public void Start()
    {
        projectileSpeed = Random.Range(5f, 10f);
        _myTransform = this.transform;
        if (instance.asteroidPhaseEnd)
        {
            targetLocation = instance.binCollision.transform;
        }
        else
        {
            targetLocation = instance.shipCollisionBox.transform;
        }
        StartCoroutine(Life());
    }

    public void Update()
    {
        if (instance.asteroidPhaseEnd)
        {
            targetLocation = instance.binCollision.transform;
        }
        transform.LookAt(targetLocation.position);
        _myTransform.position +=  _myTransform.forward * projectileSpeed * Time.deltaTime;
        
    }
    
    //This is just in case there are stray asteroids that dont collide or get destroyed. 
    private IEnumerator Life()
    {
        yield return new WaitForSeconds(30f);
        GameManager.Instance.asteroidsSpawned.Remove(gameObject);
        GameManager.Instance.asteroidSpawner.asteroids.Remove(gameObject);
        Destroy(gameObject);
    }
}
