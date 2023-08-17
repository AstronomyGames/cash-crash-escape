using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject hapticsMark;
    [SerializeField] private Image hapticsImage;
    [SerializeField] private Color grayColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private GameObject AudioMark;
    [SerializeField] private Image AudioImage;

    private void Awake()
    {
        InitializeButtons();
    }

    public void StartGame()
    {
        HCStandards.Game.StartGame();
    }


    public void LoadNextLevel()
    {
        LoadManager.instance.LoadNextLevel();
    }

    private void InitializeButtons()
    {
        hapticsMark.SetActive(!HCStandards.Haptics.isHapticesEnabled);
        AudioMark.SetActive(!HCStandards.Audio.isAudioEnabled);

        hapticsImage.color = HCStandards.Haptics.isHapticesEnabled ? greenColor : grayColor;
        AudioImage.color = HCStandards.Audio.isAudioEnabled ? greenColor : grayColor;
    }

    public void HapticsButton()
    {
        hapticsMark.SetActive(!hapticsMark.activeInHierarchy);
        HCStandards.Haptics.isHapticesEnabled = !hapticsMark.activeInHierarchy;
        hapticsImage.color = HCStandards.Haptics.isHapticesEnabled ? greenColor : grayColor;
    }

    public void AudioButton()
    {
        AudioMark.SetActive(!AudioMark.activeInHierarchy);
        HCStandards.Audio.isAudioEnabled = !AudioMark.activeInHierarchy;
        AudioImage.color = HCStandards.Audio.isAudioEnabled ? greenColor : grayColor;

    }

    public void DebugMenu()
    {

    }
}
