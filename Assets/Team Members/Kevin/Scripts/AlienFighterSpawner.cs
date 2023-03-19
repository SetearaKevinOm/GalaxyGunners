using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienFighterSpawner : MonoBehaviour
{
    public SpawnManager spawnManager;
    public int spawnAmount;
    
    [SerializeField]private BoxCollider boxCollider;
    [SerializeField]private Vector3 cubeSize;
    [SerializeField]private Vector3 cubeCentre;

    public void OnEnable()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        cubeSize = boxCollider.size;
        cubeCentre = boxCollider.transform.position;
    }

    public void Start()
    {
        StartCoroutine(SpawnAlienFighters());
    }

    private IEnumerator SpawnAlienFighters()
    {
        yield return new WaitForSeconds(5f);
        
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(spawnManager.alienFightersPrefab, Randomizer(), Quaternion.identity);

        }

        StartCoroutine(SpawnAlienFighters());
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
