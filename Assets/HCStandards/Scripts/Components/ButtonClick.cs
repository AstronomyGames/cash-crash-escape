using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityEvent onClick;

    private bool animateUp;

    private Transform tr;

    private Vector3 currentScale;

    private bool startAnimate;
    public bool oneTime;

    [HideInInspector] public bool once;

    private void Start()
    {
        tr = transform;
    }


    private bool firstDown;
    private void OnEnable()
    {
        firstDown = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CallFunction();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!firstDown)
        {
            currentScale = transform.localScale;
            firstDown = true;
        }
        startAnimate = true;
        animateUp = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animateUp = true;
    }

    private void LateUpdate()
    {
        if (!startAnimate)
        {
            return;
        }

        if (!animateUp)
        {
            tr.localScale = Vector3.MoveTowards(tr.localScale, currentScale * 0.835f, Time.unscaledDeltaTime * 3);
        }
        else
        {
            tr.localScale = Vector3.MoveTowards(tr.localScale, currentScale, Time.unscaledDeltaTime * 3);
            if (tr.localScale == currentScale)
            {
                animateUp = false;
                startAnimate = false;
            }
        }
    }


    private void CallFunction()
    {
        if (oneTime)
        {
            if (once)
            {
                return;
            }
        }
        onClick.Invoke();
        once = true;
    }

}

