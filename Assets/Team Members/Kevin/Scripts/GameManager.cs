using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Liminal.SDK.Core;
using Liminal.Core.Fader;
namespace Kevin
{
    public class GameManager : MonoBehaviour
    {
        [Header("Component References")]
        public UIManager uiManager;

        [Header("GameObject References")] 
        public AsteroidSpawner asteroidSpawner;
        public DialogueManager dialogueManager;
        public VisualizerScript visualizerScript;
        public GameObject vrAvatar;
        public CameraShake cameraShake;
        public GameObject shipCollisionBox;
        public GameObject enemyThreshold;
        public GameObject tutorialTargets;
        public GameObject handConnectors;
        
        [Header("Game State Variables")] 
        public int shipHealth;
        public int currentPlayerDamage;
        
        public int maxRequiredAsteroids;
        public int currentAsteroidsDestroyed;
        
        public bool rightHandConnected;
        public bool leftHandConnected;
        public bool bothHandsConnected;

        public int tutorialTargetCount;
        public bool tutorialStart;
        public bool tutorialEnd;
        
        
        [Header("Power Ups")] 
        public bool quickFire;
        public bool buckShot;
        public bool laserBeam;
        
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

        public IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            visualizerScript.track = dialogueManager.gameDialogue[0];
            visualizerScript.PlayAudioClip();
            StartCoroutine(ShowConnectors());
        }

        public void PlayNextScript()
        {
            if(visualizerScript.currentAudioIndex >=7)return;
            visualizerScript.currentAudioIndex++;
            visualizerScript.PlayAudioClip();
        }

        private IEnumerator ShowConnectors()
        {
            yield return new WaitForSeconds(6f);
            handConnectors.SetActive(true);
        }

        public void EndGame()
        {
            if (currentAsteroidsDestroyed >= maxRequiredAsteroids && tutorialEnd == false)
            {
                tutorialEnd = true;
                PlayNextScript();
                //Debug.Log("Ending Game!");
                /*var fader = ScreenFader.Instance;
                fader.FadeTo(Color.black,1f);
                ExperienceApp.End();*/
            }
        }

        public void SpawnAsteroidBegin()
        {
            PlayNextScript();
            asteroidSpawner.BeginSpawn();
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

