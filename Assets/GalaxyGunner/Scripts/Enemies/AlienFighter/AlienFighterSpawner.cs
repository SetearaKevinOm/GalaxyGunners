using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienFighterSpawner : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameManager _instance;
    private BoxCollider boxCollider;
    private Vector3 cubeSize;
    private Vector3 cubeCentre;
    public int spawnTimer;
    public int maxEnemies;
    public List<GameObject> alienFighters;

    public void OnEnable()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        cubeSize = boxCollider.size;
        cubeCentre = boxCollider.transform.position;
        _instance = GameManager.Instance;
    }

    public void BeginSpawn()
    {
        StartCoroutine(SpawnAlienFighters());
    }

    private IEnumerator SpawnAlienFighters()
    {
        yield return new WaitForSeconds(spawnTimer);
        if (!_instance.alienPhaseEnd)
        {
            for (int i = 0; i < spawnManager.alienFightersPrefab.Count; i++)
            {
                if (alienFighters.Count <= maxEnemies)
                {
                    GameObject go = Instantiate(spawnManager.alienFightersPrefab[i], Randomizer(), Quaternion.identity);
                    GameObject go2 = Instantiate(spawnManager.alienFightersPrefab[i], Randomizer(), Quaternion.identity);
                    alienFighters.Add(go);
                    alienFighters.Add(go2);
                }
            }
            StartCoroutine(SpawnAlienFighters());
        }
        else if(_instance.alienPhaseEnd)
        {
            StopCoroutine(SpawnAlienFighters());
        }
        
        
    }

    private Vector3 Randomizer()
    {
        float randomInCubeX = Random.Range(-cubeSize.x/4, cubeSize.x/4);
        float randomInCubeY = Random.Range(-cubeSize.y/4, cubeSize.y/4);
        float randomInCubeZ = Random.Range(-cubeSize.z/4, cubeSize.z/4);
        Vector3 randomPositionInCube = new Vector3(randomInCubeX, randomInCubeY, randomInCubeZ);
        return cubeCentre + randomPositionInCube;
    }
}
