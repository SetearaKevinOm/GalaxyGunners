using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : EnemyBase
{
    public float randomX;
    public float randomY;
    public float randomZ;
    public void OnEnable()
    {
        randomX = Random.Range(-10f, 10f);
        randomY = Random.Range(-10f, 10f);
        randomZ = Random.Range(-10f, 10f);
    }
    public void Start()
    {
        health = 100f;
        projectileSpeed = 10f;
    }

    public void Update()
    {
        //gameObject.transform.Rotate(new Vector3(randomX,randomY,randomZ),Space.Self);
        //transform.eulerAngles = Vector3.forward * projectileSpeed * Time.deltaTime;
        transform.LookAt(GameManager.Instance.shipCollisionBox.transform.position);
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
}
