﻿using System.Collections;
using System.Collections.Generic;
using Kevin;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class RightTurret : Turrets
{
    public void Update()
    {
        turretPivotPoints.LookAt(crosshairTransform.transform.position);
        transform.rotation = handTransform.rotation;
        transform.LookAt(crosshair.transform);

        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            instance.PlayNextScript();
        }*/
        
        /*if (Input.GetKey(KeyCode.Mouse1))
        {
            turretObject.transform.Rotate(new Vector3(0,0,2.5f),Space.Self);
            //play vrrrrr sound
            if(Input.GetKey(KeyCode.Mouse1) && chargeTime < 2)
            {
                isCharging = true;
                if (isCharging)
                {
                    /*if (chargeAudioPlay)
                    {
                        chargeSFX.PlayOneShot(chargeSFX.clip);
                        chargeAudioPlay = false;
                    }#1#
                    chargeTime += Time.deltaTime * chargeSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.Mouse1) && chargeTime >= 2)
            {
                turretObject.transform.Rotate(new Vector3(0,0,7f),Space.Self);
                ShootRight();
            }
        }
        else
        {
            chargeAudioPlay = true;
            isCharging = false;
            chargeTime = 0;
            rightGunUI.GetComponent<Animator>().CrossFade("Right_BuildDown",0,0);
        }*/
                    
        /*if (Input.GetKey(KeyCode.Mouse1))
        {
            turretObject.transform.Rotate(new Vector3(0,0,2.5f),Space.Self);
            //play vrrrrr sound
            if(Input.GetKey(KeyCode.Mouse1) && chargeTime < 2)
            {
                isCharging = true;
                if (isCharging)
                {
                    /*if (chargeAudioPlay)
                    {
                        chargeSFX.PlayOneShot(chargeSFX.clip);
                        chargeAudioPlay = false;
                    }#1#
                    chargeTime += Time.deltaTime * chargeSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.Mouse1) && chargeTime >= 2)
            {
                turretObject.transform.Rotate(new Vector3(0,0,7f),Space.Self);
                ShootRight();
            }
        }
        else
        {
            chargeAudioPlay = true;
            isCharging = false;
            chargeTime = 0;
            rightGunUI.GetComponent<Animator>().CrossFade("Right_BuildDown",0,0);
        }*/
        
        var device = VRDevice.Device;
        var rightHand = device.PrimaryInputDevice;
        
        if(rightHand.GetButton(VRButton.Trigger))
        {
            turretObject.transform.Rotate(new Vector3(0,0,2.5f),Space.Self);
            //play vrrrrr sound
            if(rightHand.GetButton(VRButton.Trigger) && chargeTime < 2)
            {
                isCharging = true;
                if (isCharging)
                {
                    /*if (chargeAudioPlay)
                    {
                        chargeSFX.PlayOneShot(chargeSFX.clip);
                        chargeAudioPlay = false;
                    }*/
                    chargeTime += Time.deltaTime * chargeSpeed;
                }
            }
            else if (rightHand.GetButton(VRButton.Trigger) && chargeTime >= 2)
            {
                turretObject.transform.Rotate(new Vector3(0,0,7f),Space.Self);
                ShootRight();
            }
        }
        else
        {
            chargeAudioPlay = true;
            isCharging = false;
            chargeTime = 0;
            rightGunUI.GetComponent<Animator>().CrossFade("Right_BuildDown",0,0);
        }
    }
    #region ShootRight

    void ShootRight()
    {
	    rightGunUI.GetComponent<Animator>().CrossFade("Right_BuildUP",0,0);
        if (canShoot)
        {
            //instance.TriggerVibration(shootSFX.clip,OVRInput.Controller.RTouch);
            /*GameObject go = instance.objectPool.GetPooledObject();
            if (go != null)
            {
                go.transform.position = balistics.transform.position;
                Quaternion.LookRotation(balistics.transform.forward);
                go.GetComponent<Projectile>().balisticsTransform = balistics.transform;
                go.SetActive(true);
            }*/
            GameObject go = Instantiate(projectilePrefab, balistics.transform.position,
                Quaternion.LookRotation(balistics.transform.forward));
            
            go.GetComponent<Projectile>().balisticsTransform = balistics.transform;
            StartCoroutine(RFireRateDelay());
            flashParticle.Play();
            shootSFX.PlayOneShot(shootSFX.clip);
            canShoot = false;
        }
    }
    private IEnumerator RFireRateDelay()
    {
        if (halfFireRate) fireRate = 0.3f;
        if (rapidFireRate) fireRate = 0.2f;
        if (rapidrapidFireRate) fireRate = 0.1f;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    #endregion
}
