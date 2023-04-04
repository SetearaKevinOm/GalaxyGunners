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
    public TextMeshProUGUI asteroidText;
    private GameManager _instance;
    public void Start()
    {
        _instance = GameManager.Instance;
        shipHealthSlider.maxValue = _instance.shipHealth;
    }
    public void Update()
    {
        shipHealthSlider.value = _instance.shipHealth;
        healthText.text = _instance.shipHealth.ToString();
        if (_instance.currentAsteroidsDestroyed < _instance.maxRequiredAsteroids)
        {
            asteroidText.text = _instance.currentAsteroidsDestroyed.ToString();
        }
        else if (_instance.currentAsteroidsDestroyed >= _instance.maxRequiredAsteroids)
        {
            asteroidText.text = _instance.maxRequiredAsteroids.ToString();
        }
        
    }
}
