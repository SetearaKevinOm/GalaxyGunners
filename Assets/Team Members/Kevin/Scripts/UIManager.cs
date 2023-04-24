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
    public GameObject uiShipRef;
    public Image turretStatus;
    public Image hyperDriveStatus;
    public TextMeshProUGUI oxygenLevels;
    public float oxygenLevel = 100f;
    
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

    public void Update()
    {
        oxygenLevel -= Time.deltaTime/30;
        oxygenLevels.text = ((int)oxygenLevel).ToString();
    }

    private void UpdateShipHealth()
    {
        shipHealthSlider.value = _instance.shipHealth;
        /*float currentHealthPercentage = _instance.shipHealth / _instance.maxShipHealth;
        Debug.Log("Current percentage is: " + currentHealthPercentage);
        Debug.Log("Current Ship Health is " + _instance.shipHealth); 
        Debug.Log("Current MAX Health is " + _instance.maxShipHealth);*/
        healthText.text = (_instance.shipHealth/100).ToString();
        //Debug.Log("Current percentage is: " + currentHealthPercentage);
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
