using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.GetComponentInParent<AlienFighters>().gameObject == null) return;
        other.GetComponentInParent<AlienFighters>().gameObject.SetActive(false);
        other.gameObject.SetActive(false);*/
        if (other.gameObject != null)
        {
            other.gameObject.SetActive(false);
        }
    }
}
