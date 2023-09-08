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
    public GameObject coalEffectPrefab;
    public AudioSource steamSoundEffect;
    public AudioSource clickSoundEffect;


    public float rotationSpeed = 35;
    public int playerHandicap = 100;
    public float score = 1;

    private readonly float startRotation = 320f;
    private readonly float maxZ = 37f;
    private readonly int maxScore = 100000;
    private int timer = 30;
    private bool isGameOver = false;
    private bool starsSystem = false;
    private Vector3 buttonLocation;
    private float fullAudioVolume;
    private float lowAudioVolume = 0.05f;
    private float targetAudioVolume;
    private float speedOfVolume = 1f;
    private string code;
    private int finalScore;
    private int topScoreThershold = 3800;
    private int minScoreThershold = 2000;
    private string playerDifficulty;

    private void Start()
    {
        StartCoroutine("CountdownTimer");
        scoreText.text = "Score: " + score.ToString();
        timerText.text = "Time Remaining: " + timer.ToString();
        targetAudioVolume = steamSoundEffect.volume;
        fullAudioVolume = targetAudioVolume;
        steamSoundEffect.loop = true;
        steamSoundEffect.volume = lowAudioVolume;
        steamSoundEffect.Play();
        playerDifficulty = PlayerPrefs.GetString("code");

        if(playerDifficulty == "28aea")
        {
            topScoreThershold = 2000;
            minScoreThershold = 1000;
        }
        else if(playerDifficulty == "28bjw")
        {
            topScoreThershold = 1500;
            minScoreThershold = 700;
        }
    }
    private void Update()
    {
        if (needle.transform.localEulerAngles.z <= maxZ)
        {
            needle.transform.localRotation = Quaternion.Euler(0, 0, maxZ);
        }

        if (needle.transform.localEulerAngles.z <= startRotation && !isGameOver)
        {
            needle.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        if (needle.transform.localEulerAngles.z < 240 && needle.transform.localEulerAngles.z > 210 && score <= maxScore && !isGameOver)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            targetAudioVolume = fullAudioVolume;
        }
        else
        {
            targetAudioVolume = lowAudioVolume;
        }

        if (timer >= 0)
        {
            timerText.text = "Time Remaining: " + timer.ToString();
        }

        if (timer == 0 && !isGameOver && !starsSystem)
        {
            StopCoroutine("CountdownTimer");
            isGameOver = true;
            targetAudioVolume = lowAudioVolume;
        }

        if (isGameOver && !starsSystem)
        {

            finalScore = CalculateFinalScore();

            if(finalScore >= 2)
            {
                code = "Code is: 8745";
            }
            else
            {
                code = "Code is: 2256";
            }

            starsMoveToScreen.CycleItemClick(finalScore, code);
            starsSystem = true;
            steamSoundEffect.Stop();
        }

        if (!starsSystem)
        {
            steamSoundEffect.volume = Mathf.Lerp(steamSoundEffect.volume, targetAudioVolume, speedOfVolume * Time.deltaTime);
        }
    }

    public void PlayerRotate()
    {
        if (!isGameOver)
        {
            buttonLocation = new Vector3(Random.Range(-2f, 3f), 2.5f, 0f);
            needle.transform.Rotate(0, 0, -rotationSpeed * playerHandicap * Time.deltaTime);
            Instantiate(coalEffectPrefab, buttonLocation, Quaternion.identity);
            clickSoundEffect.Play();
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

    private int CalculateFinalScore()
    {
        if (score >= topScoreThershold)
        {
            return 3;
        }
        else if (score >= minScoreThershold && score < topScoreThershold)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
