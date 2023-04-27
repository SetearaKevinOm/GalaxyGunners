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
        oxygenLevel -= Time.deltaTime / 30f;
        oxygenLevels.text = ((int)oxygenLevel).ToString();
        // this code will probably never be need because the oxygen levels stay at a high number usually and is only used for visual juice.
        if (oxygenLevel >= 80f) oxygenLevels.color = Color.green;
        if (oxygenLevel >= 30 && oxygenLevel < 80) oxygenLevels.color = Color.yellow;
        if (oxygenLevel < 30) oxygenLevels.color = Color.red;
    }

    private void UpdateShipHealth()
    {
        if(_instance.shipHealth >= 8000) healthText.color = Color.green;
        if (_instance.shipHealth > 3000 && _instance.shipHealth < 8000) healthText.color = Color.yellow;
        if (_instance.shipHealth <= 3000) healthText.color = Color.red;
        
        //On the off chance the player loses all their HP, place holder for now it just runs the end experience function
        if(_instance.shipHealth < 1) _instance.EndGamePhase();
        
        shipHealthSlider.value = _instance.shipHealth;
        healthText.text = (_instance.shipHealth/100).ToString();
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
