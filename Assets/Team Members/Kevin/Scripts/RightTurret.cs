using System.Collections;
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
    #region ShootRight

    void ShootRight()
    {
        if (canShoot)
        {
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
            //instance.TriggerVibration(shootSFX.clip,OVRInput.Controller.RTouch);
            go.GetComponent<Projectile>().myColor = ColorEnum.MyColor.Red;
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
                    
                    currentTurretDamage = GameManager.Instance.currentPlayerDamage;
                    enemyBase.OnClicked(currentTurretDamage);
                }
            }
            canShoot = false;
        }
    }
    private IEnumerator RFireRateDelay()
    {
        if (halfFireRate) fireRate = 0.25f;
        if (rapidFireRate) fireRate = 0.05f;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    #endregion
}
