using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : EnemyBase
{
    public void Start()
    {
        projectileSpeed = Random.Range(10f, 15f);
        StartCoroutine(Life());
    }

    public void Update()
    {
        //gameObject.transform.Rotate(new Vector3(randomX,randomY,randomZ),Space.Self);
        //transform.eulerAngles = Vector3.forward * projectileSpeed * Time.deltaTime;
        transform.LookAt(instance.shipCollisionBox.transform.position);
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private IEnumerator Life()
    {
        yield return new WaitForSeconds(30f);
    }
}
