using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiddenItems : MonoBehaviour
{
    public Image[] hiddenItemsDisplay;
    public Button[] hiddenItemsButtons;
    public TMP_Text timerText;
    public int remainingItems;
    public int timer = 20;
    public bool isGameFinished = false;
    public int topTimerThreshold = 15;
    public int minimumTimerThreshold = 5;

    public StarsMoveToScreen starsMoveToScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CountdownTimer");
        remainingItems = hiddenItemsDisplay.Length;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time Remaining: " + timer.ToString();

        if (timer == 0)
        {
            StopCoroutine("CountdownTimer");
        }

        if (remainingItems == 0 && !isGameFinished)
        {
            StopCoroutine("CountdownTimer");
            isGameFinished = true;
            starsMoveToScreen.CycleItemClick(CalculateTimer(), "Code_HI_Game");
        }

    }

    public void HiddenItemClicked(int index)
    {
        hiddenItemsDisplay[index].enabled = false;
        Destroy(hiddenItemsButtons[index].gameObject);
        remainingItems--;
    }

    private int CalculateTimer()
    {
        if(timer >= 15)
        {
            return 3;
        }else if(timer >=5 && timer < 15)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    IEnumerator CountdownTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
    }

}
