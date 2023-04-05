using System.Collections;
using System.Collections.Generic;
using Kevin;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class RightTurret : Turrets
{
    //public Transform turretTransform;
    public void Update()
    {
        //if (handsConnected == false) return;
        
        turretPivotPoints.LookAt(crosshairTransform.transform.position);
        //turretTransform.rotation.x = Mathf.Clamp(handTransform.rotation.x,handTransform.rotation.y,handTransform.rotation.z);
        transform.rotation = handTransform.rotation;
        Debug.Log(handTransform.rotation);
        transform.LookAt(crosshair.transform);
        
        /*RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward * range, out hitInfo))
        {
            Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.green);
            EnemyBase enemyBase = hitInfo.collider.gameObject.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                crosshairTransform.position = hitInfo.point;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * range, Color.red);
            crosshairTransform.position = defaultCrosshairTransform.position;
        }*/

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            instance.PlayNextScript();
        }
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            turretObject.transform.Rotate(new Vector3(0,0,30f),Space.Self);
            ShootRight();
        }
        
        var device = VRDevice.Device;
        var rightHand = device.PrimaryInputDevice;
        
        if(rightHand.GetButton(VRButton.Trigger))
        {
            turretObject.transform.Rotate(new Vector3(0,0,30f),Space.Self);
            ShootRight();
        }
    }

    public void HoverEnd()
    {
        handsConnected = true;
    }
    
    #region ShootRight

    void ShootRight()
    {
        if (canShoot)
        {
            GameObject go = Instantiate(projectilePrefab, balistics.transform.position,
                Quaternion.LookRotation(balistics.transform.forward));
            //instance.TriggerVibration(shootSFX.clip,OVRInput.Controller.RTouch);
            go.GetComponent<Projectile>().balisticsTransform = balistics.transform;
            StartCoroutine(RFireRateDelay());
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

    private IEnumerator RFireRateDelay()    
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    #endregion
}
