﻿using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : EnemyBase
{
    /*public float randomX;
    public float randomY;
    public float randomZ;*/
    /*public void Start()
    {
        randomX = Random.Range(-10f, 10f);
        randomY = Random.Range(-10f, 10f);
        randomZ = Random.Range(-10f, 10f);
    }*/

    public GameManager instance;
    public void OnEnable()
    {
        instance = GameManager.Instance;
    }

    public void Update()
    {
        //gameObject.transform.Rotate(new Vector3(randomX,randomY,randomZ),Space.Self);
        //transform.eulerAngles = Vector3.forward * projectileSpeed * Time.deltaTime;
        transform.LookAt(instance.shipCollisionBox.transform.position);
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
}
