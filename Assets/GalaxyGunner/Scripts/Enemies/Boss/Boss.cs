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

    /*public IEnumerator Start()
    {
        yield return new WaitForSeconds(12f);
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(beamPrefabs[0], beamTransform.position, Quaternion.identity);
        StartCoroutine(Start());
    }*/

    public void Update()
    {
        healthBar.value = gameObject.GetComponent<Boss>().health;
    }
}
