using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class AlienFighters : EnemyBase
{
    public Transform alienLaser;
    public GameObject alienProjectile;
    public int randomShootDelay;
    public GameObject forceField;
    public bool randomForceFields;

    public void Start()
    {
        if(randomForceFields) ForceFieldRandomizer();
        StartCoroutine(DelayedStart());
    }
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f);
        randomShootDelay = Random.Range(2, 5);
        ShootPlayer();
    }
    
    private void ForceFieldRandomizer()
    {
        forceField = GetComponentInChildren<ForceField>().gameObject;
        int coinFlip = Random.Range(0, 101);
        if (coinFlip <= 49)
        {
            forceField.SetActive(true);
        }
        else
        {
            forceField.SetActive(false);
        }
    }

    private void ShootPlayer()
    {
        GameObject go = Instantiate(alienProjectile, alienLaser.position,
            Quaternion.LookRotation(alienLaser.transform.forward));
        go.GetComponent<EnemyProjectile>().balisticsTransform = alienLaser;
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(randomShootDelay);
        ShootPlayer();
    }
    public void Update()
    {
        transform.LookAt(GameManager.Instance.shipCollisionBox.transform.position);
    }
}
