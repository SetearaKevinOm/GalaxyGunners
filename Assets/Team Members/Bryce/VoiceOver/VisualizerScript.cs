using System.Collections;
using System.Collections.Generic;
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

    AudioSource _audioSource;

    void Start()
    {
        visualizerObjects = GetComponentsInChildren<RectTransform>();

        if (!track)
            return;

        _audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        _audioSource.loop = loop;
        _audioSource.clip = track;
        _audioSource.Play();
    }

    void Update()
    {
        float[] spectrumData = _audioSource.GetSpectrumData(visualizerSimples, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;

            newSize.y = Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 5.0f), updateSenstivity);
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
        }
    }
}
