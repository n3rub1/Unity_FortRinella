using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInputMove : MonoBehaviour
{

    private TMP_InputField inputField;
    public int moveSpeed;
    public bool canMove = false;
    public bool stopMovement = true;
    public int rightDirection = 1;
    public int xLimitLeft = -300;
    public int xLimitRight = 1500;
    public int yLimit = 470;
    private RectTransform rectTransform;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        rectTransform = inputField.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //reset position when it reaches 0
        if (rectTransform.anchoredPosition.x <= xLimitLeft)
        {
            rectTransform.anchoredPosition = new Vector2(xLimitLeft, yLimit);
            stopMovement = true;
        }
        //reset position when it reaches 1000
        if (rectTransform.anchoredPosition.x >= xLimitRight)
        {
            rectTransform.anchoredPosition = new Vector2(xLimitRight, yLimit);
            stopMovement = true;
        }

        if (canMove && !stopMovement)
        {
            rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if (!canMove && !stopMovement)
        {
            rectTransform.anchoredPosition += new Vector2(rightDirection * moveSpeed * Time.deltaTime, 0);
        }

    }

    public void moveItem()
    {
        if (!canMove)
        {
            rectTransform.anchoredPosition = new Vector2(xLimitRight - 1, yLimit);
        }
        else
        {
            rectTransform.anchoredPosition = new Vector2(xLimitLeft + 1, yLimit);
        }

        stopMovement = false;
        canMove = !canMove;
    }
}
