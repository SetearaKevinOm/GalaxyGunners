using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider shipHealthSlider;
    public TextMeshProUGUI healthText;
    public void Start()
    {
        shipHealthSlider.maxValue = GameManager.Instance.shipHealth;
    }
    public void Update()
    {
        shipHealthSlider.value = GameManager.Instance.shipHealth;
        healthText.text = GameManager.Instance.shipHealth.ToString();
    }
}
