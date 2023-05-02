using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class BossProjectile : EnemyBase
{
    /*public Material redMat;
    public Material blueMat;
    public bool startCharge;
    public bool maxCharge;
    public void OnEnable()
    {
        instance = GameManager.Instance;
        int coinFlip = Random.Range(0, 100);
        if (coinFlip < 51) myColor = MyColor.Blue;
        else myColor = MyColor.Red;
        ChangeMaterial();
        startCharge = true;
    }

    public void ChangeMaterial()
    {
        if (myColor == MyColor.Blue)
        {
            GetComponent<MeshRenderer>().material = blueMat;
        }
        else
        {
            GetComponent<MeshRenderer>().material = redMat;
        }
    }

    public void Update()
    {
        if (instance == null) return;
        transform.LookAt(instance.shipCollisionBox.transform.position);
        if (!startCharge) return;
        if(!maxCharge) gameObject.transform.localScale += Vector3.one * Time.deltaTime/10f;
        if (gameObject.transform.localScale.x >= 0.15) gameObject.transform.position += projectileSpeed * transform.forward;
    }*/
}
