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
    public GameObject itemFoundEffect;
    public AudioSource itemFoundSoundEffect;


    private int remainingItems;
    private int timer = 20;
    private bool isGameFinished = false;
    private readonly int topTimerThreshold = 15;
    private readonly int minimumTimerThreshold = 5;
    private readonly float[] xAxisEffect = new float[] {-2f, -1.3f, -0.6f, 0.3f, 1.1f, 1.9f };
    private readonly float yAxisEffect = 4.5f;
    private string code;
    private int score;
    private string playerDifficulty;

    public StarsMoveToScreen starsMoveToScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CountdownTimer");
        playerDifficulty = PlayerPrefs.GetString("code");
        remainingItems = hiddenItemsDisplay.Length;

        if(playerDifficulty == "18aea")
        {
            timer = 20;
        }
        else if(playerDifficulty == "18bjw")
        {
            timer = 30;
        }
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

            score = CalculateTimer();
            if(score >= 2)
            {
                code = "Code is: 9898";
            }
            else
            {
                code = "Code is: 9856";
            }

            starsMoveToScreen.CycleItemClick(score, code);
        }

    }

    public void HiddenItemClicked(int index)
    {
        hiddenItemsDisplay[index].enabled = false;
        Destroy(hiddenItemsButtons[index].gameObject);
        Vector3 currentItemLocationOnCamera = new Vector3(xAxisEffect[index], yAxisEffect, 0);
        Instantiate(itemFoundEffect, currentItemLocationOnCamera, Quaternion.identity);
        itemFoundSoundEffect.Play();
        remainingItems--;
    }

    private int CalculateTimer()
    {
        if(timer >= topTimerThreshold)
        {
            ReputationStatic.SetReputationForHiddenItemsGame(5);
            ReputationStatic.SetHiddenItemsGameToTrue();
            return 3;
        }else if(timer >=minimumTimerThreshold && timer < topTimerThreshold)
        {
            ReputationStatic.SetReputationForHiddenItemsGame(2);
            ReputationStatic.SetHiddenItemsGameToTrue();
            return 2;
        }
        else
        {
            ReputationStatic.SetReputationForHiddenItemsGame(0);
            ReputationStatic.SetHiddenItemsGameToTrue();
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
