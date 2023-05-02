using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    public SpawnManager spawnManager;
    //public bool tutorialMode;
    private BoxCollider boxCollider;
    private Vector3 cubeSize;
    private Vector3 cubeCentre;
    private GameManager _instance;
    public float timer;
    public int maxEnemies;
    public List<GameObject> asteroids;
    

    public void Start()
    {
        _instance = GameManager.Instance;
        boxCollider = GetComponentInChildren<BoxCollider>();
        cubeSize = boxCollider.size;
        cubeCentre = boxCollider.transform.position;
    }
    public void BeginSpawn()
    {
        StartCoroutine(SpawnRedAsteroids());
    }

    private void Timer()
    {
        if (timer <= 5)
        {
            timer = 5;
        }
        else
        {
            timer--;
        }
    }

    private IEnumerator SpawnRedAsteroids()
    {
        yield return new WaitForSeconds(timer);
        if (!_instance.asteroidPhaseEnd)
        {
            if (_instance.currentAsteroidsDestroyed < 10)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    if (asteroids.Count <= maxEnemies)
                    {
                        GameObject go = Instantiate(spawnManager.asteroidPrefabs[0], Randomizer(), Quaternion.identity);
                        asteroids.Add(go);
                        _instance.AsteroidList(go);
                        yield return new WaitForSeconds(1f);
                    }
                }
                StartCoroutine(SpawnBlueAsteroids());
            }

            if (_instance.currentAsteroidsDestroyed >= 10)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    if (asteroids.Count <= maxEnemies)
                    {
                        GameObject go = Instantiate(spawnManager.asteroidPrefabs[0], Randomizer(), Quaternion.identity);
                        GameObject go2 = Instantiate(spawnManager.asteroidPrefabs[1], Randomizer(), Quaternion.identity);
                        asteroids.Add(go);
                        asteroids.Add(go2);
                        _instance.AsteroidList(go);
                        _instance.AsteroidList(go2);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                StartCoroutine(SpawnBlueAsteroids());
            }
            
        }
        else if (_instance.asteroidPhaseEnd)
        {
            StopCoroutine(SpawnRedAsteroids());
        }
    }
    
    private IEnumerator SpawnBlueAsteroids()
    {
        yield return new WaitForSeconds(timer);
        if (_instance.currentAsteroidsDestroyed < 10)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (asteroids.Count <= maxEnemies)
                {
                    GameObject go = Instantiate(spawnManager.asteroidPrefabs[1], Randomizer(), Quaternion.identity);
                    asteroids.Add(go);
                    _instance.AsteroidList(go);
                    yield return new WaitForSeconds(1f);
                }
            }
            StartCoroutine(SpawnRedAsteroids());
        }

        if (_instance.currentAsteroidsDestroyed >= 10)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (asteroids.Count <= maxEnemies)
                {
                    GameObject go = Instantiate(spawnManager.asteroidPrefabs[0], Randomizer(), Quaternion.identity);
                    GameObject go2 = Instantiate(spawnManager.asteroidPrefabs[1], Randomizer(), Quaternion.identity);
                    asteroids.Add(go);
                    asteroids.Add(go2);
                    _instance.AsteroidList(go);
                    _instance.AsteroidList(go2);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            Timer();
            StartCoroutine(SpawnRedAsteroids());
        }
        else if (_instance.asteroidPhaseEnd)
        {
            StopCoroutine(SpawnBlueAsteroids());
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
