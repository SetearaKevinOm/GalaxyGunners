using System.Collections;
using System.Collections.Generic;
using Kevin;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class LeftTurret : Turrets
{
    
    public void Update()
    {
        if (instance.leftHandConnected == false) return;
        
        turretPivotPoints.LookAt(crosshairTransform.transform.position);
        transform.rotation = handTransform.rotation;
        transform.LookAt(crosshair.transform);
        
        /*RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward * range, out hitInfo))
        {
            Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * range, Color.red);
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ShootLeft();
        }*/
        
        var device = VRDevice.Device;
        var leftHand = device.SecondaryInputDevice;

        if(leftHand.GetButton(VRButton.Trigger))
        {
            turretObject.transform.Rotate(new Vector3(0,0,30f),Space.Self);
            ShootLeft();
        }
    }
    #region ShootLeft
    
    public void HoverEnd()
    {
        //handsConnected = true;
    }


    void ShootLeft()
    {
        if (canShoot)
        {
            GameObject go = Instantiate(projectilePrefab, balistics.transform.position,
                Quaternion.LookRotation(balistics.transform.forward));
            //instance.TriggerVibration(shootSFX.clip,OVRInput.Controller.RTouch);
            go.GetComponent<Projectile>().balisticsTransform = balistics.transform;
            StartCoroutine(LFireRateDelay());
            flashParticle.Play();
            shootSFX.Play();
            RaycastHit hitInfo;
            if (Physics.Raycast(crosshairTransform.position, transform.forward * range, out hitInfo, range))
            {
                EnemyBase enemyBase = hitInfo.collider.gameObject.GetComponent<EnemyBase>();
                if (enemyBase != null)
                {
                    //laserLine.SetPosition(1, hitInfo.point);
                    //StartCoroutine(ShootLine());
                    currentTurretDamage = GameManager.Instance.currentPlayerDamage;
                    enemyBase.OnClicked(currentTurretDamage);
                }
                else 
                {
                    //laserLine.SetPosition(1,balistics.transform.position + (balistics.transform.forward * range));
                    //StartCoroutine(ShootLine());
                }
            }
            canShoot = false;
        }
        
    }

    private IEnumerator ShootLine()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.5f);
        laserLine.enabled = false;
    }

    private IEnumerator LFireRateDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    #endregion
}
