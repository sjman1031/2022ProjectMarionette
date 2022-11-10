using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetValure : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderVal)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);
    }

    public void SetLevel1(float sliderVal)
    {
        mixer.SetFloat("MusicVol(Btn)", Mathf.Log10(sliderVal) * 20);
    }

}
