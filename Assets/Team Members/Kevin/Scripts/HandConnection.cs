﻿using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class HandConnection : MonoBehaviour
{
    public GameManager instance;
    public void OnEnable()
    {
        instance = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        LeftHand leftHand = other.GetComponent<LeftHand>();
        RightHand rightHand = other.GetComponent<RightHand>();
        other.gameObject.SetActive(false);
        if (leftHand)
        {
            instance.leftHandConnected = true;
        }
        else if (rightHand)
        {
            instance.rightHandConnected = true;
        }
        
        if (instance.leftHandConnected && instance.rightHandConnected)
        {
            instance.bothHandsConnected = true;
            instance.PlayNextScript();
            instance.tutorialTargets.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
