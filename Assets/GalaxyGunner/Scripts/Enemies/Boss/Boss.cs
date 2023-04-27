using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    public List<GameObject> fighterPrefabs;
    public List<Transform> fighterSpawnTransforms;
    public int spawnTimer;
    public List<GameObject> beamPrefabs;
    public Transform beamTransform;
    public Slider healthBar;

    public void OnEnable()
    {
        healthBar.maxValue = gameObject.GetComponent<Boss>().health;
    }
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        Shoot();
        spawnTimer = 10;
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);
        ReleaseFighters();
    }
    private void ReleaseFighters()
    {
        for (int i = 0; i < fighterSpawnTransforms.Count; i++)
        {
            Instantiate(fighterPrefabs[Random.Range(0, fighterPrefabs.Count)], fighterSpawnTransforms[i].position, Quaternion.identity);
        }
        spawnTimer--;
        if (spawnTimer <= 4) spawnTimer = 4;
        StartCoroutine(SpawnTimer());
    }

    private void Shoot()
    {
        Instantiate(beamPrefabs[0], beamTransform.position, Quaternion.identity);
    }

    public void Update()
    {
        healthBar.value = gameObject.GetComponent<Boss>().health;
    }
}
