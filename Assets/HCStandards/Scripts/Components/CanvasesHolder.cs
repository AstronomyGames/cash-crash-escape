using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasesHolder : MonoBehaviour
{
    [SerializeField] private ModeCanvas mainCanvas, inGameCanvas, endGameCanvas;


  
}

public enum CanvasType
{
    Main,
    InGame,
    EndGame
}
