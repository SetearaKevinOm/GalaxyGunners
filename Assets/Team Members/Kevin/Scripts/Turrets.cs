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
        public LineRenderer laserLine;
        public AudioSource shootSFX;
        public GameObject projectilePrefab;
        public Transform defaultCrosshairTransform;
        public GameManager instance;
        //public bool handsConnected;
        public bool halfFireRate;
        public bool rapidFireRate;
        public bool rapidrapidFireRate;

        public bool isCharging;
        public float chargeSpeed = 3f;
        public float chargeTime;
        public bool chargeAudioPlay;

        public AudioSource chargeSFX;
        
        //UI Animation Stuff
        public GameObject leftGunUI;
        public GameObject rightGunUI;
        
        public void Start()
        {
            instance = GameManager.Instance;
            flashParticle = barrelFlash.GetComponent<ParticleSystem>();
            crosshair = gameObject.GetComponentInChildren<Crosshair>();
            laserLine = GetComponentInChildren<LineRenderer>();
            defaultCrosshairTransform = crosshairTransform;

        }
    }
}

