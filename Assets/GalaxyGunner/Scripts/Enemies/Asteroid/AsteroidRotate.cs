using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotate : MonoBehaviour
{
    public float randomX;
    public float randomY;
    public float randomZ;
    public void Start()
    {
        randomX = Random.Range(-2f, 2f);
        randomY = Random.Range(-2f, 2f);
        randomZ = Random.Range(-2f, 2f);
    }

    public void Update()
    {
        gameObject.transform.Rotate(new Vector3(randomX, randomY, randomZ), Space.Self);
    }
}
