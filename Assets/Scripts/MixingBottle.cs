using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixingBottle : MonoBehaviour, IDropHandler
{
    public Mixing mixing;
    public GameObject mixingEffectBrown;
    public GameObject mixingEffectYellow;
    public GameObject mixingEffectGreen;
    public AudioSource bubbleMixtureAudio;

    private readonly int yDeductionForPosition = 130;
    private Vector2 grenadePosition;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject droppedObject = eventData.pointerDrag;
            MixingDragAndDrop mixDragAndDrop = droppedObject.GetComponent<MixingDragAndDrop>();
            int mix = mixDragAndDrop.GetMixtureValue();
            mixDragAndDrop.SetCanBeDragged(false);

            grenadePosition = GetComponent<RectTransform>().anchoredPosition;
            grenadePosition.y -= yDeductionForPosition;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = grenadePosition;
            mixing.AddToTotalMixture(mix);

            Vector2 effectPosition = GetPositionForEffect(grenadePosition.x);
            InstantiateAccordingToColor(effectPosition, mix);

            bubbleMixtureAudio.Play();
        }
    }

    public Vector2 GetPositionForEffect(float x)
    {

        switch (x)
        {
            case -332:
                {
                    return new Vector2(-1.4f, 103);
                }
            case 4:
                {
                    return new Vector2(0.09f, 103);
                }
            case 342:
                {
                    return new Vector2(1.5f, 103);
                }
            case -171:
                {
                    return new Vector2(-0.7f, 100.9f);
                }
            case 155:
                {
                    return new Vector2(0.7f, 100.9f);
                }
            default:
                return new Vector2();
        }

    }

    public void InstantiateAccordingToColor(Vector2 effectCoordinates, int mix)
    {
        switch (mix)
        {
            case 15:
                {
                    Instantiate(mixingEffectBrown, effectCoordinates, Quaternion.identity);
                }
                break;
            case 20:
                {
                    Instantiate(mixingEffectYellow, effectCoordinates, Quaternion.identity);
                }
                break;
            case 25:
                {
                    Instantiate(mixingEffectGreen, effectCoordinates, Quaternion.identity);
                }
                break;
        }
    }

}

