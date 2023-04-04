using System.Collections;
using System.Collections.Generic;
using Kevin;
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
         if (gameObject.GetComponent<Asteroid>() != null)
         {
            GameManager.Instance.currentAsteroidsDestroyed++;
         }
         Destroy(gameObject);
      }
   }
}
