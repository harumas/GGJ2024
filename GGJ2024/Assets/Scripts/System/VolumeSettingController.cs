using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class VolumeSettingController : MonoBehaviour
{
    private Slider VolumeSlider;
    public float Volume { get; private set; }

    private void Awake()
    {
        Locator.Register(this);
    }
    private void Start()
    {
        VolumeSlider = GameObject.FindObjectOfType<Slider>();
        Volume = VolumeSlider.value;
    }
  public void VolumeChange()
    {
        Volume = VolumeSlider.value;
    }
}
