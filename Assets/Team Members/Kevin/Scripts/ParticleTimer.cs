using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour
{
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
