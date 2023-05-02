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
        StartCoroutine(SpawnRedAlienFighter());
    }

    private void Timer()
    {
        if (spawnTimer <= 5)
        {
            spawnTimer = 5;
        }
        else
        {
            spawnTimer--;
        }
    }
    private IEnumerator SpawnRedAlienFighter()
    {
        yield return new WaitForSeconds(spawnTimer);
        if (!_instance.alienPhaseEnd)
        {
            if (_instance.currentAliensDestroyed <= 15)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    if (alienFighters.Count <= maxEnemies)
                    {
                        GameObject go = Instantiate(spawnManager.alienFightersPrefab[1], Randomizer(), Quaternion.identity);
                        alienFighters.Add(go);
                        yield return new WaitForSeconds(1f);
                    }
                }
                StartCoroutine(SpawnBlueAlienFighters());
            }

            if (_instance.currentAliensDestroyed > 10)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    if (alienFighters.Count <= maxEnemies)
                    {
                        GameObject go = Instantiate(spawnManager.alienFightersPrefab[0], Randomizer(), Quaternion.identity);
                        GameObject go2 = Instantiate(spawnManager.alienFightersPrefab[1], Randomizer(), Quaternion.identity);
                        alienFighters.Add(go);
                        alienFighters.Add(go2);
                        yield return new WaitForSeconds(1f);
                    }
                }
                StartCoroutine(SpawnBlueAlienFighters());
            }
            
        }
        else if (_instance.alienPhaseEnd)
        {
            StopCoroutine(SpawnRedAlienFighter());
        }
    }
    
    private IEnumerator SpawnBlueAlienFighters()
    {
        yield return new WaitForSeconds(spawnTimer);
        if (!_instance.alienPhaseEnd)
        {
            if (_instance.currentAliensDestroyed <= 15)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    if (alienFighters.Count <= maxEnemies)
                    {
                        GameObject go = Instantiate(spawnManager.alienFightersPrefab[0], Randomizer(), Quaternion.identity);
                        alienFighters.Add(go);
                        yield return new WaitForSeconds(1f);
                    }
                }
                StartCoroutine(SpawnRedAlienFighter());
            }

            if (_instance.currentAsteroidsDestroyed > 15)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    if (alienFighters.Count <= maxEnemies)
                    {
                        GameObject go = Instantiate(spawnManager.alienFightersPrefab[0], Randomizer(), Quaternion.identity);
                        GameObject go2 = Instantiate(spawnManager.alienFightersPrefab[1], Randomizer(), Quaternion.identity);
                        alienFighters.Add(go);
                        alienFighters.Add(go2);
                        yield return new WaitForSeconds(1f);
                    }
                }
                Timer();
                StartCoroutine(SpawnRedAlienFighter());
            }
            
        }
        else if (_instance.alienPhaseEnd)
        {
            StopCoroutine(SpawnBlueAlienFighters());
        }
    }
    
    //This Function Randomly calculates a position within the desired collision box/area.
    private Vector3 Randomizer()
    {
        float randomInCubeX = Random.Range(-cubeSize.x/4, cubeSize.x/4);
        float randomInCubeY = Random.Range(-cubeSize.y/4, cubeSize.y/4);
        float randomInCubeZ = Random.Range(-cubeSize.z/4, cubeSize.z/4);
        Vector3 randomPositionInCube = new Vector3(randomInCubeX, randomInCubeY, randomInCubeZ);
        return cubeCentre + randomPositionInCube;
    }
}
