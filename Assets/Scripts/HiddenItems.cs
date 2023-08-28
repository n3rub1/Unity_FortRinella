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
        Vector3 currentItemLocationOnCamera = new Vector3(xAxisEffect[index], yAxisEffect, 0);
        Instantiate(itemFoundEffect, currentItemLocationOnCamera, Quaternion.identity);
        itemFoundSoundEffect.Play();
        remainingItems--;
    }

    private int CalculateTimer()
    {
        if(timer >= topTimerThreshold)
        {
            return 3;
        }else if(timer >=minimumTimerThreshold && timer < topTimerThreshold)
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
