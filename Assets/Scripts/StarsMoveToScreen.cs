using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarsMoveToScreen : MonoBehaviour
{

    public RawImage backgroundImage;
    public RawImage starOne;
    public RawImage starTwo;
    public RawImage starThree;
    public TMP_Text code;

    public int moveSpeed;
    private bool backgroundCanMove = false;
    private bool starOneCanMove = false;
    private bool starTwoCanMove = false;
    private bool starThreeCanMove = false;
    private bool codeTextCanMove = false;

    public int xLimitBackgroundLeft = 650;
    public int xLimitBackgroundRight = 2700;
    public int yBackgroundLimit = 110;

    public int xStarOneLimitLeft = 390;
    public int xStarOneLimitRight = 2700;
    public int yStarOneLimit = 110;

    public int xStarTwoLimitLeft = 720;
    public int xStarTwoLimitRight = 2700;
    public int yStarTwoLimit = 110;

    public int xStarThreeLimitLeft = 1070;
    public int xStarThreeLimitRight = 2700;
    public int yStarThreeLimit = 110;

    public int xCodeLimitLeft = 750;
    public int xCodeLimitRight = 1500;
    public int yCodeLimit = 100;

    public float waitBetweenStars = 1f;
    public float starTransperancy = 0.35f;

    private int rightDirection = 1;
    private int starRating;
    private Color starTwoColor;
    private Color starThreeColor;
    private string codeText;

    private void Start()
    {
        starTwoColor = starTwo.color;
        starThreeColor = starThree.color;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        backgroundCanMove = CheckLimitsImage(xLimitBackgroundRight, xLimitBackgroundLeft, yBackgroundLimit, backgroundImage);
        starOneCanMove = CheckLimitsImage(xStarOneLimitRight, xStarOneLimitLeft, yStarOneLimit, starOne);
        starTwoCanMove = CheckLimitsImage(xStarTwoLimitRight, xStarTwoLimitLeft, yStarTwoLimit, starTwo);
        starThreeCanMove = CheckLimitsImage(xStarThreeLimitRight, xStarThreeLimitLeft, yStarThreeLimit, starThree);
        codeTextCanMove = CheckLimitsText(xCodeLimitRight, xCodeLimitLeft, yCodeLimit, code);

        if (backgroundCanMove)
        {
            backgroundImage.rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if (starOneCanMove)
        {
            starOne.rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if (starTwoCanMove)
        {
            starTwo.rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if (starThreeCanMove)
        {
            starThree.rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if (codeTextCanMove)
        {
            code.rectTransform.anchoredPosition += new Vector2(-rightDirection * moveSpeed * Time.deltaTime, 0);
        }

        if (starRating == 1)
        {
            starTwoColor.a = starTransperancy;
            starTwo.color = starTwoColor;
            starThreeColor.a = starTransperancy;
            starThree.color = starThreeColor;
            code.text = codeText;
        }
        else if(starRating == 2)
        {
            starThreeColor.a = starTransperancy;
            starThree.color = starThreeColor;
            code.text = codeText;
        }
        else
        {
            code.text = codeText;
        }
        
    }

    private bool CheckLimitsImage(int xLimitRight, int xLimitLeft, int yLimit, RawImage image)
    {
        if (image.rectTransform.anchoredPosition.x <= xLimitLeft)
        {
            image.rectTransform.anchoredPosition = new Vector2(xLimitLeft, yLimit);
            return false;
        }

        if (image.rectTransform.anchoredPosition.x >= xLimitRight)
        {
            image.rectTransform.anchoredPosition = new Vector2(xLimitRight, yLimit);
            return false;
        }

        return true;
    }

    private bool CheckLimitsText(int xLimitRight, int xLimitLeft, int yLimit, TMP_Text text)
    {
        if (text.rectTransform.anchoredPosition.x <= xLimitLeft)
        {
            text.rectTransform.anchoredPosition = new Vector2(xLimitLeft, yLimit);
            return false;
        }

        if (text.rectTransform.anchoredPosition.x >= xLimitRight)
        {
            text.rectTransform.anchoredPosition = new Vector2(xLimitRight, yLimit);
            return false;
        }

        return true;
    }
    public void CycleItemClick(int starRating, string codeNumber)
    {
        StartCoroutine(CycleItemEnumerator());
        this.starRating = starRating;
        codeText = codeNumber;
    }

    public IEnumerator CycleItemEnumerator()
    {
        backgroundImage.rectTransform.anchoredPosition = new Vector2(xLimitBackgroundRight - 1, yBackgroundLimit);
        backgroundCanMove = true;
        yield return new WaitForSeconds(waitBetweenStars);
        starOne.rectTransform.anchoredPosition = new Vector2(xStarOneLimitRight - 1, yStarOneLimit);
        starOneCanMove = true;
        yield return new WaitForSeconds(waitBetweenStars);
        starTwo.rectTransform.anchoredPosition = new Vector2(xStarTwoLimitRight - 1, yStarTwoLimit);
        starTwoCanMove = true;
        yield return new WaitForSeconds(waitBetweenStars);
        starThree.rectTransform.anchoredPosition = new Vector2(xStarThreeLimitRight - 1, yStarThreeLimit);
        starThreeCanMove = true;
        yield return new WaitForSeconds(waitBetweenStars);
        code.rectTransform.anchoredPosition = new Vector2(xCodeLimitRight - 1, yCodeLimit);
        codeTextCanMove = true;
    }
}
