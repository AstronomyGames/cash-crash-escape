using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Procedural;
using DG.Tweening;


public class LoadManager : MonoBehaviour
{
    public static LoadManager instance = null;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fillAmount = 0.0f;
    [SerializeField] private ImageModifier fillImage;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject loadScreen;
    private bool startLoading = false;
    private AsyncOperation asyncOperation;
    private float fadeSpeed;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            HCStandards.DataManager.Load();
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            {
                
                return;
            }
            else
            {
                canvas.SetActive(true);
                ManageScene(true);
            }

        }
    }

    public void LoadNextLevel()
    {
        ManageScene(false);
    }

    private async void ManageScene(bool init)
    {
        await Task.Delay(50);
        if (init)
        {
            StartLoad();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }


    public void SetDesign(Color fillColor)
    {
        fillImage.color = fillColor;
    }

    private void StartLoad()
    {
        asyncOperation = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single); ;
        asyncOperation.allowSceneActivation = false;
        startLoading = true;
    }

    private void LateUpdate()
    {
        if (!startLoading)
            return;

        fillAmount = Mathf.MoveTowards(fillAmount, asyncOperation.progress / 0.9f, 10f * Time.deltaTime);
        fillImage.fillAmount = fillAmount;

        if (fillAmount >= 0.99f)
        {
            startLoading = false;
            FadeScreen();
        }
    }

    private void FadeScreen()
    {
        fadeImage.DOFade(1f, 0.4f).OnComplete(() =>
        {
            loadScreen.SetActive(false);
            asyncOperation.allowSceneActivation = true;
            fadeImage.DOFade(0f, 0.3f).OnComplete(() =>
            {
                canvas.SetActive(false);
            });
        });
    }
}
