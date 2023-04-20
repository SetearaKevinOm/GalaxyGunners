using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using UnityEngine.UI;

public class ShipDamageUI : MonoBehaviour
{
    public Sprite blueShip;
    public Sprite redShip;
    public GameObject uiRef;
    private Image currentImage;
    public GameManager managerRef;

    
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        managerRef = GetComponentInParent<GameManager>();
        currentImage = uiRef.GetComponent<Image>();
    }
    
    public void ShipDamaged()
    {
        currentImage.sprite = redShip;
        Invoke("ResetImage",0.5f);
    }

    public void ResetImage()
    {
        currentImage.sprite = blueShip;
    }
}
