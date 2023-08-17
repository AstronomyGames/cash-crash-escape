using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuController : Singleton<DebugMenuController>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetUp();

    }

    private void SetUp()
    {
        cameraPosX.text = CameraController.instance.offset.x.ToString();
        cameraPosY.text = CameraController.instance.offset.y.ToString();
        cameraPosZ.text = CameraController.instance.offset.z.ToString();

        cameraRotX.text = CameraController.instance.rotation.x.ToString();
        cameraRotY.text = CameraController.instance.rotation.y.ToString();
        cameraRotZ.text = CameraController.instance.rotation.z.ToString();

        playerRunSpeed.text = GlobalSettings.instance.playerRunSpeed.ToString();

        bulletSpeed.text = GlobalSettings.instance.bulletSpeed.ToString();
    }

    [SerializeField] private TMP_InputField cameraPosX;
    [SerializeField] private TMP_InputField cameraPosY;
    [SerializeField] private TMP_InputField cameraPosZ;

    [SerializeField] private TMP_InputField cameraRotX;
    [SerializeField] private TMP_InputField cameraRotY;
    [SerializeField] private TMP_InputField cameraRotZ;

    [SerializeField] private TMP_InputField playerRunSpeed;
    [SerializeField] private TMP_InputField bulletSpeed;


    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Apply()
    {
        Vector3 cameraOffset;
        cameraOffset.x= float.Parse(cameraPosX.text); 
        cameraOffset.y= float.Parse(cameraPosY.text); 
        cameraOffset.z= float.Parse(cameraPosZ.text);

        CameraController.instance.offset = cameraOffset;

        Vector3 cameraRotation;
        cameraRotation.x= float.Parse(cameraRotX.text);
        cameraRotation.y= float.Parse(cameraRotY.text);
        cameraRotation.z= float.Parse(cameraRotZ.text);

        CameraController.instance.rotation = cameraRotation;

        GlobalSettings.instance.playerRunSpeed= float.Parse(playerRunSpeed.text);
        GlobalSettings.instance.bulletSpeed= float.Parse(bulletSpeed.text);

        CameraController.instance.UpdateValues();
    }
}
