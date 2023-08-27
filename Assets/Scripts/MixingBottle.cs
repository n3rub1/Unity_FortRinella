using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixingBottle : MonoBehaviour, IDropHandler
{
    public Mixing mixing;
    private int yDeductionForPosition = 130;
    private Vector2 grenadePosition;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            GameObject droppedObject = eventData.pointerDrag;
            MixingDragAndDrop mixDragAndDrop = droppedObject.GetComponent<MixingDragAndDrop>();
            int mix = mixDragAndDrop.GetMixtureValue();
            mixDragAndDrop.SetCanBeDragged(false);

            grenadePosition = GetComponent<RectTransform>().anchoredPosition;
            grenadePosition.y -= yDeductionForPosition;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = grenadePosition;
            mixing.AddToTotalMixture(mix);
        }

    }
}
