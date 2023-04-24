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
        public GameObject bossShip;
        public GameObject binCollision;
        public GameObject asteroidUIPanel;
        public GameObject alienUIPanel;
        public GameObject bossUIPanel;
        public GameObject hyperdriveParticleFX1;
        public GameObject hyperdriveParticleFX2;


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
        public int turretsDestroyedCount;
        
        
        public bool isColorSchemed;
        
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
            //intro
            visualizerScript.PlayAudioClip();
            audioManager.bgmMusic.volume = dialogueVolume;
            StartCoroutine(SpawnTutorialMines());
        }

        private IEnumerator SpawnTutorialMines()
        {
            yield return new WaitForSeconds(12f);
            tutorialTargets.SetActive(true);
            uiManager.turretStatus.color = Color.green;
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
            //complete tutorial
            PlayNextScript();
            audioManager.bgmMusic.volume = dialogueVolume;
            StartCoroutine(DelayAsteroidScript());
        }
        
        private IEnumerator DelayAsteroidScript()
        {
            yield return new WaitForSeconds(0.5f);
            //start asteroid
            //PlayNextScript();
            asteroidSpawner.BeginSpawn();
            asteroidUIPanel.SetActive(true);
            leftTurret.halfFireRate = true;
            rightTurret.halfFireRate = true;
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
                asteroidPhaseEnd = true;
                StartCoroutine(SpawnAlienBegin());
            }
        }

        private IEnumerator SpawnAlienBegin()
        {
            yield return new WaitForSeconds(5f);
            PlayNextScript();
            alienFighterSpawner.BeginSpawn();
            asteroidUIPanel.SetActive(false);
            alienUIPanel.SetActive(true);
            yield return new WaitForSeconds(9f);
            leftTurret.rapidFireRate = true;
            rightTurret.rapidFireRate = true;
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
                alienPhaseEnd = true;
                StartCoroutine(StartBossPhase());
            }
        }

        private IEnumerator StartBossPhase()
        {
            yield return new WaitForSeconds(3f);
            PlayNextScript();
            yield return new WaitForSeconds(5f);
            PlayNextScript();
            bossShip.SetActive(true);
            yield return new WaitForSeconds(10f);
            alienUIPanel.SetActive(false);
            bossUIPanel.SetActive(true);
            rightTurret.rapidrapidFireRate = true;
            leftTurret.rapidrapidFireRate = true;
        }

        public void TurretDestroyed(GameObject go)
        {
            turretsDestroyedCount++;
            bossShip.GetComponent<Boss>().health -= 150;
            if (turretsDestroyedCount >= 5)
            {
                bossShip.GetComponent<Boss>().health -= 150;
                BossShipFight();
            }
        }

        private void BossShipFight()
        {
            bossShip.GetComponent<BoxCollider>().enabled = true;
        }

        public void StartEndPhase()
        {
	        bossShip.GetComponent<Animator>().SetBool("ShipDead", true);
            StartCoroutine(EndingDialogue());
        }
        
        private IEnumerator EndingDialogue()
        {
            yield return new WaitForSeconds(2f);
            uiManager.hyperDriveStatus.color = Color.green;
            PlayNextScript();
            yield return new WaitForSeconds(10f);
            StartCoroutine(HyperDriveSequence());
            
        }

        private IEnumerator HyperDriveSequence()
        {
            hyperdriveParticleFX1.SetActive(true);
            hyperdriveParticleFX2.SetActive(true);
            yield return new WaitForSeconds(7f);
            Debug.Log("Hyper!!!!");
            EndGamePhase();
        }
        private void EndGamePhase()
        {
            Debug.Log("Ending Game!");
            var fader = ScreenFader.Instance;
            fader.FadeTo(Color.black,3f);
            ExperienceApp.End();
            
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

