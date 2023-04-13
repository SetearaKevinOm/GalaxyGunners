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
    

    public void Start()
    {
        _instance = GameManager.Instance;
        boxCollider = GetComponentInChildren<BoxCollider>();
        cubeSize = boxCollider.size;
        cubeCentre = boxCollider.transform.position;
    }
    public void BeginSpawn()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(timer);
        if(!_instance.asteroidPhaseEnd)
        {
            for (int i = 0; i < spawnManager.asteroidPrefabs.Count; i++)
            {
                GameObject go = Instantiate(spawnManager.asteroidPrefabs[i], Randomizer(), Quaternion.identity);
                GameObject go2 = Instantiate(spawnManager.asteroidPrefabs[i], Randomizer(), Quaternion.identity);
                _instance.AsteroidList(go, go2);
                
            }
            StartCoroutine(SpawnAsteroids());
        }
        else if (_instance.asteroidPhaseEnd)
        {
            StopCoroutine(SpawnAsteroids());
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
