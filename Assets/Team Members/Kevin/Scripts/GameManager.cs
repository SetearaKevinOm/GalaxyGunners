using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kevin
{
    public class GameManager : MonoBehaviour
    {
        [Header("Component References")]
        public UIManager uiManager;
        
        [Header("GameObject References")]
        public GameObject vrAvatar;
        public GameObject shipCollisionBox;
        
        [Header("Game State Variables")] 
        public int shipHealth;
        public int currentPlayerDamage;

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if(_instance == null)
                    Debug.LogError("Game Manager is NULL!");
                return _instance;
            }
        }
        
        

        private void Awake()
        {
            _instance = this;
        }
    }
}

