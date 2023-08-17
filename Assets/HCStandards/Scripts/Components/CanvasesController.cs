using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasesController : MonoBehaviour
{

    [SerializeField] private ModeCanvas mainCanvas, inGameCanvas, endGameCanvas;
    [SerializeField] private UIDesign design;

    private void Awake()
    {

    }
    private void GameStarted(bool s)
    {
        mainCanvas.Disable();
        inGameCanvas.Enable(s);
    }

    private void GameEnded(bool s)
    {
        inGameCanvas.Disable();
        endGameCanvas.Enable(s);
    }

    //private void Initialize()
    //{
    //    Debug.Log("Init");
    //    switch (design)
    //    {
    //        case UIDesign.Apple:
    //            CanvasesHolder holder = (Instantiate(Resources.Load("Prefabs/UI_Design_Apple", typeof(GameObject))) as GameObject).GetComponent<CanvasesHolder>();
    //            SetUpCanvases(holder);
    //            break;
    //        case UIDesign.Lemon:
    //            break;
    //        case UIDesign.Strawberrie:
    //            break;
    //        case UIDesign.Blackberrie:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private void SetUpCanvases(CanvasesHolder holder)
    //{
    //    mainCanvas = holder.GetCanvas(CanvasType.Main);
    //    inGameCanvas = holder.GetCanvas(CanvasType.InGame);
    //    endGameCanvas = holder.GetCanvas(CanvasType.EndGame);
    //    holder.transform.parent = transform;
    //    holder.gameObject.SetActive(true);
    //    HCStandards.HCStandards.OnInitializeTaskFinished();
    //}

    private void OnEnable()
    {
        HCStandards.Game.onGameStarted += GameStarted;
        HCStandards.Game.onGameEnded += GameEnded;
    }

    private void OnDisable()
    {
        HCStandards.Game.onGameStarted -= GameStarted;
        HCStandards.Game.onGameEnded -= GameEnded;
    }
}
