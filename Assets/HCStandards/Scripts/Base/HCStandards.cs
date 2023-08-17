using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

namespace HCStandards
{
    public static class Haptics
    {
        public static bool isHapticesEnabled = true;

        public static void PlayHaptic(HapticTypes hapticType)
        {
            if (!isHapticesEnabled)
                return;
            MMVibrationManager.Haptic(hapticType);
        }

        public static void SetActive(bool active)
        {
            isHapticesEnabled = active;
        }
    }
    
    
    public static class Audio
    {
        public static bool isAudioEnabled = true;

        public static void SetActive(bool active)
        {
            isAudioEnabled = active;
        }
    }

    public static class Settings
    {
        public static float CameraScale()
        {
            float s = 1f;
            if (Mathf.Floor(Camera.main.aspect * 10) / 10f <= Mathf.Floor(9 / 16f * 10) / 10f)
            {
                s = ((720f / Screen.width) * Screen.height) / 1280f;
            }
            return s;
        }
    }

    public static class Game
    {
        public delegate void GameStatus(bool win);

        public static GameStatus onGameStarted;
        public static GameStatus onGameEnded;
        public static bool IsGameStarted = false; 

        public static void StartGame()
        {
            IsGameStarted = true;
            if (onGameStarted != null)
                onGameStarted(true);
        }

        public static void EndGame(bool win, float delay = 0f)
        {
            if (!IsGameStarted)
                return;

            IsGameStarted = false;
            if (win)
            {
                DataManager.data.Level++;
                DataManager.Save();
            }


            Sequence seq = DOTween.Sequence();
            seq.SetDelay(delay).OnComplete(() =>
            {
                if (onGameEnded != null)
                    onGameEnded(win);
                else
                    Debug.Log("Game Ended -> No Subscription");
            });
        }
    }
}

