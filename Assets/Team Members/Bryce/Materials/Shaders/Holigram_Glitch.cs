﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holigram_Glitch : MonoBehaviour
{
    public float glitchChance = 0.1f;

    Material hologramMaterial;
    WaitForSeconds glitchLoopWait = new WaitForSeconds(0.1f);
    
    IEnumerator Start()
    {
        hologramMaterial = GetComponent<Renderer>().material;
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);

            if (glitchTest <= glitchChance)
            {
                //Do Glitch
                float originalGlowIntensity = hologramMaterial.GetFloat("_GlowIntensity");
                hologramMaterial.SetFloat("_GlitchIntensity", Random.Range(0.07f, 0.1f));
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity * Random.Range(0.14f, 0.44f));
                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                hologramMaterial.SetFloat("_GlitchIntensity", 0f);
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity);
            }

            yield return glitchLoopWait;
        }
    }
}
