using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFighters : EnemyBase
{
    public void Start()
    {
        health = 100f;
    }
    
    public void OnClicked(float dmg)
    {
        health -= dmg;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
