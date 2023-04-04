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
        public CameraShake cameraShake;
        public GameObject shipCollisionBox;
        public GameObject enemyThreshold;
        
        [Header("Game State Variables")] 
        public int shipHealth;
        public int currentPlayerDamage;
        public int maxRequiredAsteroids;
        public int currentAsteroidsDestroyed;

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

        /*public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
        {
            OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);

            if (controller == OVRInput.Controller.LTouch)
            {
                OVRHaptics.LeftChannel.Preempt(clip);
            }
            else if (controller == OVRInput.Controller.RTouch)
            {
                OVRHaptics.RightChannel.Preempt(clip);
            }
        }*/
    }
}

