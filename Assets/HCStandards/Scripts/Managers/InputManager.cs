using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    private float mulX;
    public float sens;
    private float mulY;
    private float lastPosX;
    [HideInInspector]
    public float deltaX;

    private float lastPosY;
    [HideInInspector]
    public float deltaY;

    private bool getInput = true;
    private void Awake()
    {
        instance = this;
        mulX = Screen.width / 720;
        mulY = Screen.height / 1280;
        
    }

    private void Update()
    {

        if (!HCStandards.Game.IsGameStarted ||!getInput)
            return;

        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPosX = Input.mousePosition.x;
            lastPosY = Input.mousePosition.y;
        }

        if (Input.GetMouseButton(0))
        {
            float currentPositionX = Input.mousePosition.x;
            if (currentPositionX != lastPosX)
            {
                deltaX = (currentPositionX - lastPosX) * mulX * sens;
            }
            else
            {
                deltaX = 0;
            }
            lastPosX = currentPositionX;

            float currentPositionY = Input.mousePosition.y;
            if (currentPositionY != lastPosY)
            {
                deltaY = (currentPositionY - lastPosY) * mulY * sens;
            }
            else
            {
                deltaY = 0;
            }
            lastPosY = currentPositionY;

        }

        if (Input.GetMouseButtonUp(0))
        {
            deltaX = 0;
            deltaY = 0;
        }
    }

    internal void Enable(bool v)
    {
        getInput = v;
    }
}
