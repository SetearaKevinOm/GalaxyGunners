using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    public Slider healthBar;

    public void Start()
    {
        healthBar.maxValue = gameObject.GetComponent<Boss>().health;
    }

    

    public void Update()
    {
        healthBar.value = gameObject.GetComponent<Boss>().health;
    }
}
