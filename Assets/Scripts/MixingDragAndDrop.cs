using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MixingDragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public RectTransform potion;
    public CanvasGroup canvasGroup;
    public int mixValue;
    public bool canBeDragged = true;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canBeDragged)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canBeDragged)
        {
            potion.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canBeDragged)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public int GetMixtureValue()
    {
        return mixValue;
    }

    public void SetCanBeDragged(bool canBeDragged)
    {
        this.canBeDragged = canBeDragged;
    }
}
