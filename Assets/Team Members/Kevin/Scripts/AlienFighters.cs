using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFighters : EnemyBase
{
    public void Start()
    {
        health = 100f;
    }

    public void OnClicked()
    {
        Debug.Log("Bang! Bang! Bang!");
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            health -= 10f;
        }
    }
}
