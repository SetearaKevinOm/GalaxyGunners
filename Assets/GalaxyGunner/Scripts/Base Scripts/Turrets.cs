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
        public GameManager instance;
        
        [Header("GameObject Refs")]
        public GameObject turretObject;
        public GameObject balistics;
        //public GameObject barrelFlash;
        public ParticleSystem flashParticle;
        public GameObject projectilePrefab;
        //UI Animation Stuff
        public GameObject leftGunUI;
        public GameObject rightGunUI;
        
        [Header("Audio Refs")]
        public List<AudioSource> shootingSFX;
        
        [Header ("Shooting Refs")]
        public bool canShoot;
        public float chargeSpeed = 3f;
        public float chargeTime;
        
        public float fireRate;
        public bool halfFireRate;
        public bool rapidFireRate;
        public bool rapidrapidFireRate;
        public bool isCharging;
        
        [Header("Aiming Refs")]
        public Transform turretPivotPoints;
        public Transform handTransform;
        public Transform crosshairTransform;
        public Crosshair crosshair;
        public Transform defaultCrosshairTransform;
        public void Start()
        {
            instance = GameManager.Instance;
            balistics = GetComponentInChildren<TurretRayCast>().gameObject;
            flashParticle = GetComponentInChildren<ParticleSystem>();
            crosshair = gameObject.GetComponentInChildren<Crosshair>();
            defaultCrosshairTransform = crosshairTransform;
        }
    }
}

