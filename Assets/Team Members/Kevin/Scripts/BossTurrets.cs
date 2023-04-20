using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class BossTurrets : EnemyBase
{
    public int startTimer;
    public GameObject bossProjectile;
    public Transform laser;
    public GameObject rotationPoint;
    public void Update()
    {
        rotationPoint.transform.LookAt(GameManager.Instance .shipCollisionBox.transform.position);
    }

    public void Start()
    {
        startTimer = Random.Range(5, 10);
        StartCoroutine(ShootTimer());
    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(startTimer);
        ShootPlayer();
    }

    private void ShootPlayer()
    {
        if (gameObject == null) return;
        GameObject go = Instantiate(bossProjectile, laser.position,
            Quaternion.LookRotation(laser.transform.forward));
        go.GetComponent<EnemyProjectile>().balisticsTransform = laser;
        StartCoroutine(ShootTimer());
    }
}
