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
        
        /*if (Input.GetKey(KeyCode.Mouse0))
        {
            turretObject.transform.Rotate(new Vector3(0,0,2.5f),Space.Self);
            //play vrrrrr sound
            if(Input.GetKey(KeyCode.Mouse0) && chargeTime < 2)
            {
                isCharging = true;
                if (isCharging)
                {
                    chargeTime += Time.deltaTime * chargeSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0) && chargeTime >= 2)
            {
                turretObject.transform.Rotate(new Vector3(0,0,7f),Space.Self);
                ShootLeft();
            }
        }
        else 
        {
            isCharging = false;
            chargeTime = 0;
            leftGunUI.GetComponent<Animator>().CrossFade("Left_BuildDown",0,0);
        }*/
        
        var device = VRDevice.Device;
        var leftHand = device.SecondaryInputDevice;
        if (leftHand == null) return;
        if(leftHand.GetButton(VRButton.Trigger))
        {
            turretObject.transform.Rotate(new Vector3(0,0,2.5f),Space.Self);
            //play vrrrrr sound
            if(leftHand.GetButton(VRButton.Trigger) && chargeTime < 2)
            {
                isCharging = true;
                if (isCharging)
                {
                    chargeTime += Time.deltaTime * chargeSpeed;
                }
            }
            else if (leftHand.GetButton(VRButton.Trigger) && chargeTime >= 2)
            {
                turretObject.transform.Rotate(new Vector3(0,0,7f),Space.Self);
                ShootLeft();
            }
        }
        else 
        {
            isCharging = false;
            chargeTime = 0;
            leftGunUI.GetComponent<Animator>().CrossFade("Left_BuildDown",0,0);
        }
    }
    #region ShootLeft
    void ShootLeft()
    {
	    leftGunUI.GetComponent<Animator>().CrossFade("Left_BuildUP",0,0);
	    if (canShoot)
        {
            //instance.TriggerVibration(shootSFX.clip,OVRInput.Controller.RTouch);
            GameObject go = Instantiate(projectilePrefab, balistics.transform.position,
                Quaternion.LookRotation(balistics.transform.forward));
            go.GetComponent<Projectile>().balisticsTransform = balistics.transform;
            StartCoroutine(LFireRateDelay());
            flashParticle.Play();
            shootSFX.PlayOneShot(shootSFX.clip);
            canShoot = false;
        }
        
    }
    private IEnumerator LFireRateDelay()
    {
        if (halfFireRate) fireRate = 0.3f;
        if (rapidFireRate) fireRate = 0.2f;
        if (rapidrapidFireRate) fireRate = 0.1f;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    #endregion
}
