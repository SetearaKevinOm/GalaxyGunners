using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class LeftHandConnection : MonoBehaviour
{
    public GameManager instance;
    public void OnEnable()
    {
        instance = GameManager.Instance;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        LeftHand leftHand = other.GetComponent<LeftHand>();
        if (leftHand != null)
        {
            other.gameObject.SetActive(false);
            instance.leftHandConnected = true;
            gameObject.SetActive(false);
        }
        if (instance.leftHandConnected && instance.rightHandConnected)
        {
            instance.bothHandsConnected = true;
            instance.PlayNextScript();
            instance.tutorialTargets.SetActive(true);
        }
    }
}
