using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Battleship : MonoBehaviour
{
    public Button[] pickMeButtons = new Button[] { };
    public TMP_Text[] buttonGridText = new TMP_Text[] { };
    public TMP_Text aimingAtText;
    public TMP_Text timerText;
    public RawImage cardLeft;
    public RawImage cardRight;
    public TMP_Text cardLeftTitle;
    public TMP_Text cardRightTitle;
    public TMP_Text cardLeftDescription;
    public TMP_Text cardRightDescription;
    public TMP_Text shotsFiredText;
    public TMP_Text currentPositionText;

    private readonly string target = "x";
    private readonly string clear = "";
    private readonly string shot = "o";
    private readonly string hit = "h";
    private readonly string[] gridBoxText = new string[] { "AX", "BX", "CX", "DX", "EX", "FX", "AY", "BY", "CY", "DY", "EY", "FY", "ZA", "ZB", "ZC", "ZD", "ZE", "ZF" };
    private readonly string[] correctFiringProcedureTitle = new string[] { "Rotate for loading_0", "Tilt Down_1", "Load_2", "Tilt Up_3", "Rotate Back for Firing_4", "Aim_5", "Fire_6" };
    private readonly string[] correctFiringProcedureDescription = new string[] { "Rotate the gun 90deg towards the loading chamber to start loading", "Tilt down the gun to wash out ambers and start loading", "Load the gun with gunpowder and shell", "Tilt the gun back up, gun is ready and loaded", "Rotate the gun back into position ready for aiming", "Aim at target", "Pull the fuse and fire the gun!" };
    private readonly string[] currentGunPosition = new string[] {"Ready for Loading", "90deg angle", "Tilted down", "Loaded", "90 deg angle", "Facing the sea", "Facing Target"};
    private int[] indexArray;
    private int secondsPassed = 0;
    private int randomNumberLeft = 0;
    private int randomNumberRight = 0;
    private int currentCardLeft;
    private int currentCardRight;
    public int currentCorrectAnswer = 0;
    private readonly float halfOpacity = 0.5f;
    private readonly float fullOpacity = 1f;
    private Color buttonColor;
    private Color redCardColor = new Color(200f / 255f, 165f / 255f, 150f / 255f, 1f);
    private Color greenCardColor = new Color(150f / 255f, 200f / 255f, 160f / 255f, 1f);
    private Color originalCardColor = new Color(1f, 1f, 1f, 1f);
    private int previousIndex = 99;
    private int currentIndex;
    private int tempIndex;
    private int totalShotsFired = 0;
    private string aimingAt;


    private void Start()
    {
        indexArray = new int[3];
        StartCoroutine(IncreaseTimer());
        currentPositionText.text = "Gun position: " + currentGunPosition[0];
    }

    private void Update()
    {
        timerText.text = "Time: " + secondsPassed.ToString();
    }
    public void HandleClick(int index)
    {
        if (buttonGridText[index].text != shot)
        {
            indexArray = CheckGridIndex(index);
            UpdateAimingAtText(index);
            buttonGridText[indexArray[0]].text = target;

            if (indexArray[0] != indexArray[1])
            {
                buttonGridText[indexArray[1]].text = clear;
            }
        }
    }

    private int[] CheckGridIndex(int index)
    {
        currentIndex = index;

        if (previousIndex == 99)
        {
            previousIndex = currentIndex;
        }
        else
        {
            previousIndex = tempIndex;
        }

        tempIndex = currentIndex;

        int[] indexResults = new int[3];

        indexResults[0] = currentIndex;
        indexResults[1] = previousIndex;
        indexResults[2] = tempIndex;

        return indexResults;

    }

    public void CheckAnswer(string whichCard)
    {
        if (whichCard == "left")
        {
            if (currentCardLeft == currentCorrectAnswer)
            {
                currentCorrectAnswer++;
                cardLeft.color = greenCardColor;

                if (currentCorrectAnswer == 7)
                {
                    currentCorrectAnswer = 0;
                    UpdateNumberOfShotsFired();
                }
            }
            else
            {
                cardLeft.color = redCardColor;
            }
        }
        if (whichCard == "right")
        {
            if (currentCardRight == currentCorrectAnswer)
            {
                currentCorrectAnswer++;
                cardRight.color = greenCardColor;

                if (currentCorrectAnswer == 7)
                {
                    currentCorrectAnswer = 0;
                    UpdateNumberOfShotsFired();
                }
            }
            else
            {
                cardRight.color = redCardColor;
            }
        }

        UpdateGunPosition();
        UpdateButtonOpacity(false, halfOpacity);
    }

    private void UpdateGunPosition()
    {
        currentPositionText.text = "Gun position: " + currentGunPosition[currentCorrectAnswer];
    }

    private void UpdateButtonOpacity(bool isEnabled, float opactiy)
    {
        foreach (Button pickMeButton in pickMeButtons)
        {
            pickMeButton.enabled = isEnabled;
            buttonColor = pickMeButton.image.color;
            buttonColor.a = opactiy;
            pickMeButton.image.color = buttonColor;
        }

        if (isEnabled)
        {
            cardLeft.color = originalCardColor;
            cardRight.color = originalCardColor;
        }
    }

    private void UpdateAimingAtText(int index)
    {
        aimingAt = gridBoxText[index];
        aimingAtText.text = "Aiming at: " + aimingAt;
    }

    private void UpdateNumberOfShotsFired()
    {
        totalShotsFired++;
        currentCorrectAnswer = 0;
        shotsFiredText.text = "Shots Fired: " + totalShotsFired.ToString();
        int index = Array.IndexOf(gridBoxText, aimingAt);

        buttonGridText[index].text = shot;
    }

    public void ChangeCardTitleAndDescription()
    {
        randomNumberLeft = Random.Range(currentCorrectAnswer, 7);
        cardLeftTitle.text = correctFiringProcedureTitle[randomNumberLeft].Substring(0, correctFiringProcedureTitle[randomNumberLeft].IndexOf('_'));
        currentCardLeft = int.Parse(correctFiringProcedureTitle[randomNumberLeft].Substring(correctFiringProcedureTitle[randomNumberLeft].IndexOf('_') + 1));
        cardLeftDescription.text = correctFiringProcedureDescription[randomNumberLeft];

        randomNumberRight = Random.Range(currentCorrectAnswer, 7);

        if (currentCorrectAnswer != 6)
        {
            while (randomNumberLeft == randomNumberRight)
            {
                randomNumberRight = Random.Range(currentCorrectAnswer, 7);
            }
        }

        cardRightTitle.text = correctFiringProcedureTitle[randomNumberRight].Substring(0, correctFiringProcedureTitle[randomNumberRight].IndexOf('_'));
        currentCardRight = int.Parse(correctFiringProcedureTitle[randomNumberRight].Substring(correctFiringProcedureTitle[randomNumberRight].IndexOf('_') + 1));
        cardRightDescription.text = correctFiringProcedureDescription[randomNumberRight];

        UpdateButtonOpacity(true, fullOpacity);
    }

    IEnumerator IncreaseTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            secondsPassed++;
        }
    }


}