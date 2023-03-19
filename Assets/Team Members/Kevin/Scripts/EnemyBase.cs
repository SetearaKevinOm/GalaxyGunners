using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
   public float health;
   public float projectileSpeed;
   
   public void OnClicked(float dmg)
   {
      health -= dmg;
      if (health <= 0f)
      {
         Destroy(gameObject);
      }
   }
}
