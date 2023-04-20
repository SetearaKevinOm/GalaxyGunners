using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class Boss : EnemyBase
{
    public GameObject redFighterPrefab;
    public GameObject blueFighterPrefab;

    public Transform redFighterHangar;
    public Transform blueFighterHangar;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnTimer());
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
}
