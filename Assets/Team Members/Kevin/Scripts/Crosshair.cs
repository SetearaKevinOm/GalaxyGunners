﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * 2.5f);
    }
}
