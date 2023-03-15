using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kevin
{
    public class Turrets : MonoBehaviour
    {
        public GameObject turretPivotPoints;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ShootTurret();
            }
        }

        public void ShootTurret()
        {
            //raycasting
        }
    }
}

