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
    public TextMeshProUGUI alienText;
    private GameManager _instance;
    
    public void OnEnable()
    {
        _instance = GetComponentInParent<GameManager>();
        shipHealthSlider.maxValue = _instance.shipHealth;
        UpdateShipHealth();
        _instance.shipCollisionBox.GetComponent<ShipCollision>().shipTakeDamage += UpdateShipHealth;
        _instance.OnAsteroidDestroyed += UpdateAsteroidCount;
        _instance.OnAlienDestroyed += UpdateAlientCount;
    }

    public void OnDisable()
    {
        _instance.shipCollisionBox.GetComponent<ShipCollision>().shipTakeDamage -= UpdateShipHealth;
        _instance.OnAsteroidDestroyed -= UpdateAsteroidCount;
        _instance.OnAlienDestroyed -= UpdateAlientCount;
    }

    private void UpdateShipHealth()
    {
        shipHealthSlider.value = _instance.shipHealth;
        healthText.text = _instance.shipHealth.ToString();
    }

    private void UpdateAsteroidCount()
    {
        if (_instance.currentAsteroidsDestroyed < _instance.maxRequiredAsteroids)
        {
            asteroidText.text = _instance.currentAsteroidsDestroyed.ToString();
        }
        else if (_instance.currentAsteroidsDestroyed >= _instance.maxRequiredAsteroids)
        {
            asteroidText.text = _instance.maxRequiredAsteroids.ToString();
        }
    }

    private void UpdateAlientCount()
    {
        if (_instance.currentAliensDestroyed < _instance.maxRequiredAliens)
        {
            alienText.text = _instance.currentAliensDestroyed.ToString();
        }
        else if (_instance.currentAliensDestroyed >= _instance.maxRequiredAliens)
        {
            alienText.text = _instance.maxRequiredAliens.ToString();
        }
    }
}
