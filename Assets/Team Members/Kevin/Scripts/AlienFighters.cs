using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlienFighters : EnemyBase
{
    public Transform alienLaser;
    public GameObject alienProjectile;
    public int randomShootDelay;
    
    public GameObject warpVFX;
    public Transform currentLookAtPosition;

    public bool inCombat;
    public bool singleRef;
    public float speed;
    
    
    public void Start()
    {
        warpVFX.GetComponent<ParticleSystem>().Play();
        AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        currentLookAtPosition = GameManager.Instance.shipCollisionBox.transform;
        inCombat = true;
        singleRef = true;
        StartCoroutine(DelayedStart());
    }
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f);
        randomShootDelay = Random.Range(3, 10);
        speed = 45f;
        ShootPlayer();
    }
    
    private void ShootPlayer()
    {
        if (gameObject == null) return;
        if (inCombat == false) return;
        GameObject go = Instantiate(alienProjectile, alienLaser.position,
            Quaternion.LookRotation(alienLaser.transform.forward));
        go.GetComponent<EnemyProjectile>().balisticsTransform = alienLaser;
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(randomShootDelay);
        if (gameObject != null) ShootPlayer();
    }
    public void Update()
    {
        if (GameManager.Instance == null) return;
        transform.LookAt(currentLookAtPosition);
        
        if (GameManager.Instance.alienPhaseEnd && singleRef)
        {
            currentLookAtPosition = GameManager.Instance.binCollision.transform;
            inCombat = false;
            singleRef = false;
        }
        
        if (inCombat == false)
        {
            Moving();
        }
    }
    
    private void Moving()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }
}
