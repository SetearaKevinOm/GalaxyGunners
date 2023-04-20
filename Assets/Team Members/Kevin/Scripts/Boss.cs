using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    public GameObject redFighterPrefab;
    public GameObject blueFighterPrefab;

    public Transform redFighterHangar;
    public Transform blueFighterHangar;

    public Slider healthBar;

    public void OnEnable()
    {
        healthBar.maxValue = gameObject.GetComponent<Boss>().health;
    }
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

    public void Update()
    {
        healthBar.value = gameObject.GetComponent<Boss>().health;
    }
}
