using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
   public int health;
   public int enemyDamage;
   public float projectileSpeed;
   public AudioClip impactSound;
   public ParticleSystem impactParticle;
   public void OnClicked(int dmg)
   {
      Debug.Log("Hit!");
      health -= dmg;
      if (gameObject.GetComponent<Asteroid>() != null)
      {
         AudioSource.PlayClipAtPoint(impactSound, GameManager.Instance.vrAvatar.transform.position);
         Instantiate(impactParticle, gameObject.transform.position, Quaternion.identity);
      }
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
