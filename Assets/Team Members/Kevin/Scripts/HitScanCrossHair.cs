using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR.Pointers;
using UnityEngine;

namespace Kevin
{
    public class HitScanCrossHair : MonoBehaviour
    {
        public Transform primaryHand;
        public float range;
        public int currentTurretDamage;
        public void Update()
        {
            transform.rotation = primaryHand.rotation;
            transform.LookAt(GetComponentInChildren<HitScanCrossHair>().gameObject.transform);
            
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward * range, out hitInfo))
            {
                Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.green);
                
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * range, Color.red);
            }
        
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }

            if (VRDevice.Device.PrimaryInputDevice.GetButtonDown(VRButton.Primary))
            {
                Shoot();
            }
        }
    
        void Shoot()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward * range, out hitInfo, range))
            {
                Debug.Log("HIT: " + hitInfo.transform.name);
                AlienFighters alienFighters = hitInfo.collider.gameObject.GetComponent<AlienFighters>();
                if (alienFighters != null)
                {
                    currentTurretDamage = GameManager.Instance.currentPlayerDamage;
                    alienFighters.OnClicked(currentTurretDamage);
                }
            }
        
        }
    }
}

