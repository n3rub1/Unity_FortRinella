using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ImageMoveToScreen : MonoBehaviour
{
    public RawImage card;
    public TMP_Text getCardText;
    public int moveSpeed;
    public bool canMove = false;
    public bool stopMovement = true;
    public int rightDirection = 1;
    public int xLimitLeft = -300;
    public int xLimitRight = 1500;
    public int yLimit = 470;


    // Update is called once per frame
    void LateUpdate()
    {
        //reset position when it reaches 0
        if (card.rectTransform.anchoredPosition.x <= xLimitLeft)
        {
            card.rectTransform.anchoredPosition = new Vector2(xLimitLeft, yLimit);
            stopMovement = true;
        }
        //reset position when it reaches 1000
        if (card.rectTransform.anchoredPosition.x >= xLimitRight)
        {
            card.rectTransform.anchoredPosition = new Vector2(xLimitRight, yLimit);
            stopMovement = true;
        }

        if (canMove && !stopMovement)
        {
            card.rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if (!canMove && !stopMovement)
        {
            card.rectTransform.anchoredPosition += new Vector2(rightDirection * moveSpeed * Time.deltaTime, 0);
        }

    }

    public void MoveItem()
    {
        if (!canMove)
        {
            card.rectTransform.anchoredPosition = new Vector2(xLimitRight - 1, yLimit);
            getCardText.text = "Remove Cards";
        }
        else
        {
            card.rectTransform.anchoredPosition = new Vector2(xLimitLeft + 1, yLimit);
            getCardText.text = "Get Random Cards";
        }

        stopMovement = false;
        canMove = !canMove;
    }

    public void CycleItemClick()
    {
        StartCoroutine(CycleItemEnumerator());
    }

    public IEnumerator CycleItemEnumerator()
    {
        MoveItem();
        yield return new WaitForSeconds(1f);
        MoveItem();

    }
}
