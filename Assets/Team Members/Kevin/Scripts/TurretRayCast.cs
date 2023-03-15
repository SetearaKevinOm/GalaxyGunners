using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Kevin
{
    public class TurretRayCast : MonoBehaviour
    {
        public bool raycastOn;
        public float range;
        void Update()
        {
            if (!raycastOn) return;
            Debug.DrawRay(transform.position, transform.forward * range, Color.red);
        }
    }

}
