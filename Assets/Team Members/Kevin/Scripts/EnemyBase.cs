using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

/*public enum EnemyColor
{
   Red,
   Blue
}*/
public class EnemyBase : ColorEnum
{
   public GameManager instance;
   public int health;
   public int enemyDamage;
   public float projectileSpeed;
   public AudioClip impactSound;
   public ParticleSystem impactParticle;
   public ParticleSystem explosionParticle;
   public MyColor myColor;
   
   public void OnEnable()
   {
      instance = GameManager.Instance;
   }
   public void OnClicked(int dmg)
   {
      if (instance == null)
      {
         instance = GameManager.Instance;
      }
      Debug.Log("Hit!");
      health -= dmg;
      AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
      Instantiate(impactParticle, gameObject.transform.position, Quaternion.identity);
      /*if (gameObject.GetComponent<Asteroid>() != null)
      {
         AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
         Instantiate(impactParticle, gameObject.transform.position, Quaternion.identity);
      }

      if (gameObject.GetComponent<TutorialTargets>() != null)
      {
         AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
         Instantiate(impactParticle, gameObject.transform.position, Quaternion.identity);
        
      }*/
         
      if (health <= 0f)
      {
         AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
         Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
         if (gameObject.GetComponent<Asteroid>() != null)
         {
            //AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            //Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            instance.AsteroidDestroyed();
            instance.EndGame();
         }

         if (gameObject.GetComponent<TutorialTargets>() != null)
         {
            //AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            //Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            if (instance.tutorialTargetCount < 3)
            {
               instance.tutorialTargetCount++;
            }
            if (instance.tutorialTargetCount >= 3)
            {
               instance.tutorialStart = true;
               instance.SpawnAsteroidBegin();
            }
         }
         Destroy(gameObject);
      }
   }
}
