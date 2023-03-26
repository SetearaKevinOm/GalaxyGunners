using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomCameraTracker : MonoBehaviour
{
    public Camera originalHeadCamera;
    void Start()
    {
        transform.position = originalHeadCamera.transform.position;
    }

}
