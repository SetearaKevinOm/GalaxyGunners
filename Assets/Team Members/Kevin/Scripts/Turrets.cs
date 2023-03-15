using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.iOS;

namespace Kevin
{
    public class Turrets : MonoBehaviour
    {
        public Transform turretPivotPoints;
        public Transform crossHair;

        public void Update()
        {
            turretPivotPoints.LookAt(crossHair);
        }
    }
}

