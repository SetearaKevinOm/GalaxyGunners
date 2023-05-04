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
        public LeftTurret leftTurret;
        public RightTurret rightTurret;
        public AudioManager audioManager;
        
        [Header("GameObject References")]
        public GameObject vrAvatar;
        public GameObject shipCollisionBox;
        public GameObject tutorialTargets;
        public GameObject bossShip;
        public GameObject binCollision;
        public GameObject asteroidUIPanel;
        public GameObject alienUIPanel;
        public GameObject bossUIPanel;
        public GameObject hyperDriveParticleFX1;
        public GameObject hyperDriveParticleFX2;
        public AudioSource endingWarp;

        [Header("Game State Variables")] 
        public int shipHealth;
        public float currentProjectileSpeed;
        public List<float> projectileSpeedPhases;
        
        [Header("Runtime Volume Control")]
        public float gameplayVolume;
        public float dialogueVolume;
        
     
        
        [Header("Game State Logic")]
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

        #region Asteroid Phase

        public void SpawnAsteroidBegin()
        {
            PlayNextScript();
            audioManager.bgmMusic.volume = dialogueVolume;
            StartCoroutine(DelayAsteroidScript());
        }
        
        private IEnumerator DelayAsteroidScript()
        {
            yield return new WaitForSeconds(0.5f);
            asteroidSpawner.BeginSpawn();
            asteroidUIPanel.SetActive(true);
            currentProjectileSpeed = projectileSpeedPhases[0];
            leftTurret.halfFireRate = true;
            rightTurret.halfFireRate = true;
            yield return new WaitForSeconds(asteroidSpawner.timer);
            audioManager.bgmMusic.volume = gameplayVolume;
        }
        public void AsteroidDestroyed(GameObject go)
        {
            asteroidsSpawned.Remove(go);
            asteroidSpawner.asteroids.Remove(go);
            currentAsteroidsDestroyed++;
            OnAsteroidDestroyed.Invoke();
        }
        
        public List<GameObject> asteroidsSpawned;
        public void AsteroidList(GameObject go)
        {
            asteroidsSpawned.Add(go);
            
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

        #endregion

        #region AlienPhase

        private IEnumerator SpawnAlienBegin()
        {
            yield return new WaitForSeconds(5f);
            PlayNextScript();
            alienFighterSpawner.BeginSpawn();
            asteroidUIPanel.SetActive(false);
            alienUIPanel.SetActive(true);
            yield return new WaitForSeconds(alienFighterSpawner.spawnTimer);
            leftTurret.rapidFireRate = true;
            rightTurret.rapidFireRate = true;
            currentProjectileSpeed = projectileSpeedPhases[1];
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

        #endregion

        #region BossPhase

         private IEnumerator StartBossPhase()
        {
            yield return new WaitForSeconds(3f);
            audioManager.bgmMusic.volume = dialogueVolume;
            PlayNextScript();
            yield return new WaitForSeconds(5f);
            PlayNextScript();
            bossShip.SetActive(true);
            yield return new WaitForSeconds(10f);
            audioManager.bgmMusic.volume = gameplayVolume;
            alienUIPanel.SetActive(false);
            bossUIPanel.SetActive(true);
            rightTurret.rapidrapidFireRate = true;
            leftTurret.rapidrapidFireRate = true;
            currentProjectileSpeed = projectileSpeedPhases[2];
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
	        yield return new WaitForSeconds(7f);
            audioManager.bgmMusic.volume = dialogueVolume;
	        uiManager.hyperDriveStatus.color = Color.green;
	        PlayNextScript();
            audioManager.bgmMusic.volume = dialogueVolume / 2f;
            StartCoroutine(HyperDriveSequence());
            
        }

        private IEnumerator HyperDriveSequence()
        {
	        yield return new WaitForSeconds(4f);
            hyperDriveParticleFX1.SetActive(true);
            hyperDriveParticleFX2.SetActive(true);
            audioManager.bgmMusic.volume = dialogueVolume / 2f;
            yield return new WaitForSeconds(7f);
            endingWarp.PlayOneShot(endingWarp.clip);
            audioManager.bgmMusic.volume = 0f;
            yield return new WaitForSeconds(5f);
            EndGamePhase();
        }
        public void EndGamePhase()
        {
            var fader = ScreenFader.Instance;
            fader.FadeTo(Color.black,3f);
            ExperienceApp.End();
        }

        #endregion

        #endregion
        

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

