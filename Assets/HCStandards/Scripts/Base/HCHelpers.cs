using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HCHelpers : Singleton<HCHelpers>
{
#if UNITY_EDITOR
    [SerializeField] private bool catchScreenShot = false;
    [SerializeField] private int currentScreenShot = 0;
    protected override void Awake()
    {
        base.Awake();

        if(catchScreenShot)
        {
            currentScreenShot= PlayerPrefs.GetInt("SC", 0);
        }
        
    }
    private void Update()
    {
        if (HCStandards.Game.IsGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                HCStandards.Game.EndGame(true, 0f);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                HCStandards.Game.EndGame(false, 0f);
            }
        }

        if(catchScreenShot)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                ScreenCapture.CaptureScreenshot("Assets/ScreenShots/SC " + currentScreenShot+".png",1);
                currentScreenShot++;
                PlayerPrefs.SetInt("SC", currentScreenShot);
                Debug.Log(Application.persistentDataPath);
            }
        }
    }

#endif

}
