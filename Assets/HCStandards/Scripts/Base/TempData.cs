using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HCStandards
{
    public class TempData : MonoBehaviour
    {
        private void LoadData()
        {
            if (PlayerPrefs.HasKey("Haptics"))
            {
                Haptics.SetActive(ConvertToBool(PlayerPrefs.GetInt("Haptics")));
            }
            else
            {
                Haptics.SetActive(true);
            }


            if (PlayerPrefs.HasKey("Audio"))
            {
                Audio.SetActive(ConvertToBool(PlayerPrefs.GetInt("Audio")));
            }
            else
            {
                Audio.SetActive(true);
            }
        }

        private bool ConvertToBool(int value)
        {
            if (value == 0)
                return false;
            else
                return true;
        }

        private int ConvertToInt(bool value)
        {
            if (value)
                return 1;
            else
                return 0;
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt("Haptics", ConvertToInt(Haptics.isHapticesEnabled));
            PlayerPrefs.SetInt("Audio", ConvertToInt(Audio.isAudioEnabled));
        }

        private void OnEnable()
        {
            LoadData();
        }

        private void OnDisable()
        {
            SaveData();
        }

        private void OnApplicationPause(bool pause)
        {
            SaveData();
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }
    }
}

