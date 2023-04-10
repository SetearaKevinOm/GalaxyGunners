using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class AlienFighters : EnemyBase
{
    public Transform alienLaser;
    public GameObject alienProjectile;
    public int randomShootDelay;
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        randomShootDelay = Random.Range(2, 5);
        ShootPlayer();
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
