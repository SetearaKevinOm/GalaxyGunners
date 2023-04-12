using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class Boss : EnemyBase
{
    public GameObject bossProjectile;
    public GameObject redFighterPrefab;
    public GameObject blueFighterPrefab;

    public Transform redFighterHangar;
    public Transform blueFighterHangar;
    public List<Transform> cannonPos;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnTimer());
        StartCoroutine(ShootTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(5f);
        ReleaseFighters();
    }
    public void ReleaseFighters()
    {
        Instantiate(redFighterPrefab, redFighterHangar.position, Quaternion.identity);
        Instantiate(blueFighterPrefab, blueFighterHangar.position, Quaternion.identity);
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(5f);
        ShootPlayer();
    }

    public void ShootPlayer()
    {
        for (int i = 0; i < cannonPos.Count; i++)
        {
            Instantiate(bossProjectile, cannonPos[i].position, Quaternion.identity);
        }

        StartCoroutine(ShootTimer());
    }
    void Update()
    {
        transform.LookAt(GameManager.Instance.vrAvatar.transform);
    }
}
