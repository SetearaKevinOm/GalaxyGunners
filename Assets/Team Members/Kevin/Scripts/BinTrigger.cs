using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        other.GetComponentInParent<AlienFighters>().gameObject.SetActive(false);
    }
}
