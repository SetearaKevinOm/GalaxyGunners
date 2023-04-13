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
        public AsteroidSpawner asteroidSpawner;
        public AlienFighterSpawner alienFighterSpawner;
        public DialogueManager dialogueManager;
        public VisualizerScript visualizerScript;
        public RubbishBin rubbishBinScript;
        public ObjectPool objectPool;
        public LeftTurret leftTurret;
        public RightTurret rightTurret;
        public AudioManager audioManager;
        
        [Header("GameObject References")]
        public GameObject vrAvatar;
        public CameraShake cameraShake;
        public GameObject shipCollisionBox;
        public GameObject enemyThreshold;
        public GameObject tutorialTargets;
        public GameObject targetTransformR;
        public GameObject targetTransformL;


        [Header("Game State Variables")] 
        public int shipHealth;
        public int currentPlayerDamage;
        public float gameplayVolume;
        public float dialogueVolume;
        
        [Header("Game State Logic")]
        public bool tutorialStart;
        public bool asteroidPhaseEnd;
        public bool alienPhaseEnd;
        
        public int tutorialTargetCount;
        public int currentAsteroidsDestroyed;
        public int maxRequiredAsteroids;
        public int currentAliensDestroyed;
        public int maxRequiredAliens;
        
        
        public bool isColorSchemed;
        
        [Header("Power Ups")] 
        public bool quickFire;
        public bool buckShot;
        public bool laserBeam;
        
        public Action OnAsteroidDestroyed;
        public Action OnAlienDestroyed;
        
        public static GameManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            uiManager = GetComponentInChildren<UIManager>();
            asteroidSpawner = GetComponentInChildren<AsteroidSpawner>();
            alienFighterSpawner = GetComponentInChildren<AlienFighterSpawner>();
            dialogueManager = GetComponentInChildren<DialogueManager>();
            rubbishBinScript = GetComponentInChildren<RubbishBin>();
            objectPool = GetComponentInChildren<ObjectPool>();
            audioManager = GetComponentInChildren<AudioManager>();
        }
        
        public IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            visualizerScript.track = dialogueManager.gameDialogue[0];
            visualizerScript.PlayAudioClip();
            audioManager.bgmMusic.volume = dialogueVolume;
            StartCoroutine(SpawnTutorialMines());
        }

        private IEnumerator SpawnTutorialMines()
        {
            yield return new WaitForSeconds(12f);
            tutorialTargets.SetActive(true);
            audioManager.bgmMusic.volume = gameplayVolume;
        }

        public void PlayNextScript()
        {
            if(visualizerScript.currentAudioIndex >=dialogueManager.gameDialogue.Count)return;
            visualizerScript.currentAudioIndex++;
            visualizerScript.PlayAudioClip();
        }

        

        #region Game Phases

        public void SpawnAsteroidBegin()
        {
            PlayNextScript();
            audioManager.bgmMusic.volume = dialogueVolume;
            StartCoroutine(DelayAsteroidScript());
            
        }
        
        private IEnumerator DelayAsteroidScript()
        {
            yield return new WaitForSeconds(6f);
            PlayNextScript();
            asteroidSpawner.BeginSpawn();
            yield return new WaitForSeconds(7f);
            audioManager.bgmMusic.volume = gameplayVolume;
        }
        public void AsteroidDestroyed(GameObject go)
        {
            asteroidsSpawned.Remove(go);
            asteroidSpawner.asteroids.Remove(go);
            currentAsteroidsDestroyed++;
            OnAsteroidDestroyed.Invoke();
        }
        public void EndAsteroidPhase()
        {
            if (currentAsteroidsDestroyed >= maxRequiredAsteroids && asteroidPhaseEnd == false)
            {
                audioManager.bgmMusic.volume = dialogueVolume;
                //Get Rid of remnants
                asteroidPhaseEnd = true;
                PlayNextScript();
                //ClearAsteroids();
                StartCoroutine(SpawnAlienBegin());
            }
        }

        private void ClearAsteroids()
        {
            for (int i = 0; i <= asteroidsSpawned.Count; i++)
            {
                asteroidsSpawned.Remove(asteroidsSpawned[i]);
                Destroy(asteroidsSpawned[i]);
            }
        }

        private IEnumerator SpawnAlienBegin()
        {
            yield return new WaitForSeconds(5f);
            PlayNextScript();
            alienFighterSpawner.BeginSpawn();
            yield return new WaitForSeconds(9f);
            leftTurret.halfFireRate = true;
            rightTurret.halfFireRate = true;
            audioManager.bgmMusic.volume = gameplayVolume;
        }
        public void AlienDestroyed(GameObject go)
        {
            alienFighterSpawner.alienFighters.Remove(go);
            currentAliensDestroyed++;
            OnAlienDestroyed.Invoke();
        }
        public void EndAlienPhase()
        {
            if (currentAliensDestroyed >= maxRequiredAliens && alienPhaseEnd == false)
            {
                //Get Rid of remnants
                alienPhaseEnd = true;
                PlayNextScript();
                Debug.Log("Alien Phase Ended");
                //start boss phase
            }
        }

        public void EndBossPhase()
        {
            //end boss sequence
            //end game
        }

        #endregion
        
        public List<GameObject> asteroidsSpawned;
        public void AsteroidList(GameObject go, GameObject go2)
        {
            asteroidsSpawned.Add(go);
            asteroidsSpawned.Add(go2);
        }

        /*public void EndGame()
        {
            //Debug.Log("Ending Game!");
            /*var fader = ScreenFader.Instance;
            fader.FadeTo(Color.black,1f);
            ExperienceApp.End();#1#
        }*/
        
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

