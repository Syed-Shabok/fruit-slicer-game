using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{   
    public Slider volumeSlider;
    public AudioSource audioSource;
    //public AudioClip music;

    // Start is called before the first frame update
    void Start()
    {   
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        audioSource.volume = volumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {   
        volumeSlider.onValueChanged.AddListener((sliderValue) => {
            PlayerPrefs.SetFloat("Volume", sliderValue);
            audioSource.volume = PlayerPrefs.GetFloat("Volume");
        });
    }
}
