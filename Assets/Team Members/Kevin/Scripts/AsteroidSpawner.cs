using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    public SpawnManager spawnManager;
    
    private BoxCollider boxCollider;
    private Vector3 cubeSize;
    private Vector3 cubeCentre;

    public void OnEnable()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        cubeSize = boxCollider.size;
        cubeCentre = boxCollider.transform.position;
    }

    public void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(5f);
        
        for (int i = 0; i < spawnManager.asteroidPrefabs.Count; i++)
        {
            Instantiate(spawnManager.asteroidPrefabs[i], Randomizer(), Quaternion.identity);
        }

        StartCoroutine(SpawnAsteroids());
    }

    private Vector3 Randomizer()
    {
        float randomInCubeX = Random.Range(-cubeSize.x/2, cubeSize.x/2);
        float randomInCubeY = Random.Range(-cubeSize.y/2, cubeSize.y/2);
        float randomInCubeZ = Random.Range(-cubeSize.z/2, cubeSize.z/2);
        Vector3 randomPositionInCube = new Vector3(randomInCubeX, randomInCubeY, randomInCubeZ);
        return cubeCentre + randomPositionInCube;
    }
}
