using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Engine : MonoBehaviour
{
    public RawImage needle;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public Button playerButton;
    public StarsMoveToScreen starsMoveToScreen;

    public float rotationSpeed = 40;
    public int playerHandicap = 100;

    private readonly float startRotation = 320f;
    private readonly float maxZ = 37f;
    private readonly int maxScore = 100000;
    private int timer = 30;
    private bool isGameOver = false;
    private bool starsSystem = false;

    public float score = 1;

    private void Start()
    {
        StartCoroutine("CountdownTimer");
        scoreText.text = "Score: " + score.ToString();
        timerText.text = "Time Remaining: " + timer.ToString();
    }
    private void Update()
    {
        if(needle.transform.localEulerAngles.z <= maxZ)
        {
            needle.transform.localRotation = Quaternion.Euler(0, 0, maxZ);
        }

        if(needle.transform.localEulerAngles.z <= startRotation && !isGameOver)
        {
            needle.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        if(needle.transform.localEulerAngles.z < 240 && needle.transform.localEulerAngles.z > 210 && score <= maxScore)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
        }

        if(timer >= 0)
        {
            timerText.text = "Time Remaining: " + timer.ToString();
        }

        if(timer == 0 && !isGameOver && !starsSystem)
        {
            Debug.Log("Game Over");
            StopCoroutine("CountdownTimer");
            isGameOver = true;
        }

        if (isGameOver && !starsSystem)
        {
            starsMoveToScreen.CycleItemClick(2, "Code From Engine");
            starsSystem = true;
        }
    }

    public void PlayerRotate()
    {
        if (!isGameOver)
        {
            needle.transform.Rotate(0, 0, -rotationSpeed * playerHandicap * Time.deltaTime);
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
