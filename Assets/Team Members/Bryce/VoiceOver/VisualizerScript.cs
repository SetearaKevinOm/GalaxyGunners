using System;
using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;

public class VisualizerScript : MonoBehaviour
{
    public float minHeight;
    public float maxHeight;
    public float updateSenstivity;
    [Space(15)]
    public RectTransform[] visualizerObjects;
    [Space(15)]
    public AudioClip track;
    public bool loop;
    [Space(15)]
    public int visualizerSimples;

    public List<AudioSource> dialogueList;
    public int currentAudioIndex;
    AudioSource _audioSource;

    public GameManager instance;

    public void OnEnable()
    {
        instance = GameManager.Instance;
    }

    public void PlayAudioClip()
    {
        visualizerObjects = GetComponentsInChildren<RectTransform>();

        if (!track) return;
        _audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        //_audioSource.loop = loop;
        track = instance.dialogueManager.gameDialogue[currentAudioIndex];
        _audioSource.clip = track;
        _audioSource.volume = 1f;
        _audioSource.priority = 1;
        _audioSource.Play();
    }

    void Update()
    {
        if (_audioSource == null) return;
        float[] spectrumData = _audioSource.GetSpectrumData(visualizerSimples, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;

            newSize.y = Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 5.0f), updateSenstivity);
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
        }
    }
}
