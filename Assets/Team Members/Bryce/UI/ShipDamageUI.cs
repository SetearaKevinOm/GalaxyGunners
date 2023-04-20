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
    public Light warningLight;
    public AudioSource dmgSFX;
    
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        managerRef = GetComponentInParent<GameManager>();
        currentImage = uiRef.GetComponent<Image>();
    }
    
    public void ShipDamaged()
    {
        currentImage.sprite = redShip;
        warningLight.gameObject.SetActive(true);
        dmgSFX.Play();
        StartCoroutine(ResetState());
    }

    private IEnumerator ResetState()
    {
        yield return new WaitForSeconds(0.5f);
        currentImage.sprite = redShip;
        yield return new WaitForSeconds(0.5f);
        currentImage.sprite = blueShip;
        warningLight.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        currentImage.sprite = redShip;
        yield return new WaitForSeconds(0.5f);
        currentImage.sprite = blueShip; 
        yield return new WaitForSeconds(0.5f);
        currentImage.sprite = redShip;
        yield return new WaitForSeconds(0.5f);
        currentImage.sprite = blueShip;
    }
}
