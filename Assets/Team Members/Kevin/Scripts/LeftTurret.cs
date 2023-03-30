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
        turretPivotPoints.LookAt(crosshairTransform.transform.position);
        transform.rotation = handTransform.rotation;
        transform.LookAt(crosshair.transform);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward * range, out hitInfo))
        {
            Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * range, Color.red);
        }
        
        /*if (Input.GetKey(KeyCode.Mouse0))
        {
            ShootLeft();
        }*/
        
        var device = VRDevice.Device;
        var leftHand = device.SecondaryInputDevice;

        if(leftHand.GetButton(VRButton.Trigger))
        {
            ShootLeft();
        }
    }
    #region ShootLeft

    void ShootLeft()
    {
        if (canShoot)
        {
            turretObject.transform.Rotate(new Vector3(0,0,30f),Space.Self);
            //StartCoroutine(FireRateDelay());
            flashParticle.Play();
            //shootSFX.PlayOneShot(shootSFX.clip);
            RaycastHit hitInfo;
            if (Physics.Raycast(balistics.transform.position, transform.forward * range, out hitInfo, range))
            {
                EnemyBase enemyBase = hitInfo.collider.gameObject.GetComponent<EnemyBase>();
                if (enemyBase != null)
                {
                    //laserLine.SetPosition(1, hitInfo.point);
                    //StartCoroutine(ShootLine());
                    currentTurretDamage = GameManager.Instance.currentPlayerDamage;
                    enemyBase.OnClicked(currentTurretDamage);
                }
            }
            else 
            {
                //laserLine.SetPosition(1,balistics.transform.position + (balistics.transform.forward * range));
                //StartCoroutine(ShootLine());
            }
            //canShoot = false;
        }
        
    }

    private IEnumerator ShootLine()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.5f);
        laserLine.enabled = false;
    }

    private IEnumerator FireRateDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    #endregion
}
