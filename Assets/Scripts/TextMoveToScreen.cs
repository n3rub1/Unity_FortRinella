using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMoveToScreen : MonoBehaviour
{

    private TMP_Text textToMove;
    public int moveSpeed;
    public bool canMove = false;
    public bool stopMovement = true;
    public int rightDirection = 1;
    public int xLimitLeft = -300;
    public int xLimitRight = 1500;
    public int yLimit = 470;

    void Start()
    {
        textToMove = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //reset position when it reaches 0
        if(textToMove.rectTransform.anchoredPosition.x <= xLimitLeft)
        {
            textToMove.rectTransform.anchoredPosition = new Vector2(xLimitLeft, yLimit);
            stopMovement = true;
        }
        //reset position when it reaches 1000
        if (textToMove.rectTransform.anchoredPosition.x >= xLimitRight)
        {
            textToMove.rectTransform.anchoredPosition = new Vector2(xLimitRight, yLimit);
            stopMovement = true;
        }

        if (canMove && !stopMovement)
        {
            textToMove.rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if(!canMove && !stopMovement)
        {
            textToMove.rectTransform.anchoredPosition += new Vector2(rightDirection * moveSpeed * Time.deltaTime, 0);
        }

    }

    public void moveItem()
    {
        if (!canMove)
        {
        textToMove.rectTransform.anchoredPosition = new Vector2(xLimitRight - 1 , yLimit);
        }
        else
        {
            textToMove.rectTransform.anchoredPosition = new Vector2(xLimitLeft + 1, yLimit);
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
        moveItem();
        yield return new WaitForSeconds(1f);
        moveItem();

    }
}
