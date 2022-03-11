// SoundVolumeSlider.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Slider))]
public class SoundVolumeSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private string mixerVarName;

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();

        // set min and max for the slider
        slider.minValue = MixerToSlider(-80.0f);
        slider.maxValue = MixerToSlider(0.0f);
    }

    void OnEnable()
    {
        // set the value for the slider to the currently set volume
        mixer.GetFloat(mixerVarName, out float currentVolume);
        slider.SetValueWithoutNotify(MixerToSlider(currentVolume));
    }

    public void OnSliderChanged(float value)
    {
        mixer.SetFloat(mixerVarName, SliderToMixer(value));
    }

    private static float SliderToMixer(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20.0f;
    }

    private static float MixerToSlider(float mixerValue)
    {
        return Mathf.Pow(10.0f, mixerValue / 20.0f);
    }
}
