using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
   public int health;
   public int enemyDamage;
   public float projectileSpeed;
   
   public void OnClicked(int dmg)
   {
      Debug.Log("Hit!");
      health -= dmg;
      if (health <= 0f)
      {
         Destroy(gameObject);
      }
   }
}
