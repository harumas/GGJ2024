using System;
using System.Linq;
using Player;
using UnityEngine;
using Utility;

public class TitleMicController : MonoBehaviour
{
    [SerializeField] private string m_DeviceName;
    private AudioClip m_AudioClip;
    private int m_LastAudioPos;
    private float m_AudioLevel;

    [SerializeField] private GameObject m_Cube;
    [SerializeField, Range(10, 500)] private float m_AmpGain = 1;

    [SerializeField] private float GameoverVolume;

    void Start()
    {
        string targetDevice = "";

        foreach (var device in Microphone.devices)
        {
            Debug.Log($"Device Name: {device}");
            if (device.Contains(m_DeviceName))
            {
                targetDevice = device;
                break;
            }
        }

        Debug.Log($"=== Device Set: {targetDevice} ===");
        m_AudioClip = Microphone.Start(targetDevice, true, 10, 48000);


    }

    void Update()
    {

        float[] waveData = GetUpdatedAudio();
        if (waveData.Length == 0)
        {
            return;
        }
        m_AudioLevel = waveData.Average(Mathf.Abs);
        m_Cube.transform.localScale = new Vector3(1, 1 + m_AmpGain * m_AudioLevel, 1);
    }

    private float[] GetUpdatedAudio()
    {
        int nowAudioPos = Microphone.GetPosition(null); // nullでデフォルトデバイス

        float[] waveData = Array.Empty<float>();

        if (m_LastAudioPos < nowAudioPos)
        {
            int audioCount = nowAudioPos - m_LastAudioPos;
            waveData = new float[audioCount];
            m_AudioClip.GetData(waveData, m_LastAudioPos);
        }
        else if (m_LastAudioPos > nowAudioPos)
        {
            int audioBuffer = m_AudioClip.samples * m_AudioClip.channels;
            int audioCount = audioBuffer - m_LastAudioPos;

            float[] wave1 = new float[audioCount];
            m_AudioClip.GetData(wave1, m_LastAudioPos);

            float[] wave2 = new float[nowAudioPos];
            if (nowAudioPos != 0)
            {
                m_AudioClip.GetData(wave2, 0);
            }

            waveData = new float[audioCount + nowAudioPos];
            wave1.CopyTo(waveData, 0);
            wave2.CopyTo(waveData, audioCount);
        }

        m_LastAudioPos = nowAudioPos;

        return waveData;
    }
    public void AmpGainChange()
    {
        VolumeSettingController volumeSettingController = Locator.Resolve<VolumeSettingController>();
        m_AmpGain = volumeSettingController.Volume;
    }

}