using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBase : ColorEnum
{
   public GameManager instance;
   public int health;
   public int enemyDamage;
   public float projectileSpeed;
   public AudioClip impactSound;
   public AudioClip spawnSound;
   public ParticleSystem impactParticle;
   public ParticleSystem explosionParticle;
   public MyColor myColor;
   public AudioClip wrongColorSFX;
   
   public void OnEnable()
   {
      instance = GameManager.Instance;
   }

   public void WrongColor(Transform projectilePosition)
   {
      //Debug.Log("Wrong Color!");
      if (wrongColorSFX == null) return;
      AudioSource.PlayClipAtPoint(wrongColorSFX, GameManager.Instance.vrAvatar.transform.position);
      //play sound 
      //show ui
   }
   public void OnClicked(int dmg, Transform projectilePosition)
   {
      if (instance == null)
      {
         instance = GameManager.Instance;
      }
      health -= dmg;
      //Debug.Log(health);
      if (impactSound == null) return;
      AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
      Instantiate(impactParticle, projectilePosition.position, Quaternion.identity);
      
         
      if (health <= 0f)
      {
         
         if (gameObject.GetComponent<Asteroid>() != null)
         {
            AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            instance.AsteroidDestroyed(gameObject);
            instance.EndAsteroidPhase();
            Destroy(gameObject);
         }

         if (gameObject.GetComponent<TutorialTargets>() != null)
         {
            AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            if (instance.tutorialTargetCount < 4)
            {
               instance.tutorialTargetCount++;
            }
            if (instance.tutorialTargetCount >= 4)
            {
               instance.tutorialStart = true;
               instance.SpawnAsteroidBegin();
            }
            Destroy(gameObject);
         }
         
         if (gameObject.GetComponent<ForceField>() != null)
         {
            AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            Instantiate(explosionParticle, projectilePosition.position, Quaternion.identity);
            Destroy(gameObject);
         }
         
         if (gameObject.GetComponent<AlienFighters>() != null)
         {
            AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            instance.AlienDestroyed(gameObject);
            instance.EndAlienPhase();
            Destroy(gameObject);
         }
         if (gameObject.GetComponent<BossTurrets>() != null)
         {
            AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            instance.TurretDestroyed(gameObject);
            Destroy(gameObject);
         }

         if (gameObject.GetComponent<Boss>() != null)
         {
            AudioSource.PlayClipAtPoint(impactSound, instance.vrAvatar.transform.position);
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            instance.StartEndPhase();
            Destroy(gameObject);
         }
      }
   }
}
