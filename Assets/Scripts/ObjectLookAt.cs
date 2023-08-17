using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectLookAt : MonoBehaviour
{
    [SerializeField] private bool once = false;

    Transform tr;

    private void Start()
    {
        tr=transform;
        tr.LookAt(CameraController.instance.mainCamera.transform);
    }

    private void Update()
    {
        if (!HCStandards.Game.IsGameStarted || !once)
            return;

        tr.LookAt(CameraController.instance.mainCamera.transform);
    }
}
