using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR.Pointers;
using UnityEditor;
using UnityEngine;

namespace Kevin
{
    public class Turrets : MonoBehaviour
    {
        public GameObject turretObject;
        public Transform turretPivotPoints;
        public Transform crosshairTransform;
        public GameObject balistics;
        public Transform handTransform;
        public float range;
        public int currentTurretDamage;
        public GameObject barrelFlash;
        public ParticleSystem flashParticle;
        public Crosshair crosshair;
        public float fireRate;
        public bool canShoot;
        public bool handTrigger;
        public LineRenderer laserLine;
        public AudioSource shootSFX;

        public void Start()
        {
            flashParticle = barrelFlash.GetComponent<ParticleSystem>();
            crosshair = gameObject.GetComponentInChildren<Crosshair>();
            laserLine = GetComponentInChildren<LineRenderer>();
        }
    }
}

