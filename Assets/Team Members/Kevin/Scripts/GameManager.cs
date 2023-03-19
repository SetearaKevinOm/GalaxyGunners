using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kevin
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public int currentPlayerDamage;
        public GameObject vrAvatar;

        public GameObject shipCollisionBox;

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

