using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Semaphore : MonoBehaviour
{

    private bool firstSequence = true;
    private bool secondSequence = false;
    private bool thirdSequence = false;
    private readonly string[] semaphoreFlag = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "_" };
    private readonly int[] codeSequenceFirstPart = new int[] { 3, 4, 2, 14, 24, 26 };            //decoy + space
    private readonly int[] codeSequenceSecondPart = new int[] { 18, 16, 20, 0, 17, 4, 26 };      //square + space
    private readonly int[] codeSequenceThirdPart = new int[] { 24, 3 };                          //yd
    private int currentIndexToUpdatePlayersInputs = 0;
    private int maxEachSequence;
    private int startIndexForEachSequence = 0;
    private bool isPlayerTurn = false;
    private List<int> playersInputs;
    private int numberOfMistakes = 0;
    private string code;
    private int score;
    private string playerDifficulty;
    private int maxMistakeThreshold = 5;
    private int minMistakeThreshold = 10;

    public Image[] alphabet;
    public float waitTime = 1f;
    public TMP_Text playerText;
    public TMP_Text startButtonText;
    public StarsMoveToScreen starsMoveToScreen;
    public AudioSource flagSoundEffect;
    public AudioSource buttonSoundEffect;


    private void Start()
    {
        playersInputs = new List<int>(new int[codeSequenceFirstPart.Length + codeSequenceSecondPart.Length + codeSequenceThirdPart.Length]);
        playerDifficulty = PlayerPrefs.GetString("code");

        if(playerDifficulty == "24aea")
        {
            SetOpacityBasedOnDifficulty(playerDifficulty);
            maxMistakeThreshold = 5;
            minMistakeThreshold = 10;
        }
        else if(playerDifficulty == "24bjw")
        {
            SetOpacityBasedOnDifficulty(playerDifficulty);
            maxMistakeThreshold = 7;
            minMistakeThreshold = 13;
        }
    }

    public void StartGame()
    {
        isPlayerTurn = false;
        startIndexForEachSequence = 0;
        if (firstSequence && !secondSequence)
        {
            buttonSoundEffect.Play();
            currentIndexToUpdatePlayersInputs = 0;
            StartCoroutine(IterateWithDelay());
        }

        if (secondSequence && !thirdSequence)
        {
            buttonSoundEffect.Play();
            firstSequence = false;
            currentIndexToUpdatePlayersInputs = codeSequenceFirstPart.Length;
            StartCoroutine(IterateWithDelay());
        }

        if (thirdSequence)
        {
            buttonSoundEffect.Play();
            firstSequence = false;
            secondSequence = false;
            currentIndexToUpdatePlayersInputs = codeSequenceFirstPart.Length + codeSequenceSecondPart.Length;
            StartCoroutine(IterateWithDelay());
        }


        if (!thirdSequence && !secondSequence && !firstSequence)
        {
            score = CheckPlayersMistakes();

            if(score >= 2)
            {
                code = "Code is: 5212";
            }
            else
            {
                code = "Code is: 6658";
            }

            starsMoveToScreen.CycleItemClick(score, code);
        }
    }

    private IEnumerator IterateWithDelay()
    {
        if (firstSequence)
        {
            startButtonText.text = "Wait & Memorize...";
            maxEachSequence = codeSequenceFirstPart.Length;
            foreach (int i in codeSequenceFirstPart)
            {
                IterationBlock(i, true);
                yield return new WaitForSeconds(waitTime);
                IterationBlock(i, false);
                yield return new WaitForSeconds(waitTime);

            }
            secondSequence = true;
            isPlayerTurn = true;
            startButtonText.text = "Enter Sequence & Start Next";
        }

        if (secondSequence && !firstSequence && !thirdSequence)
        {
            startButtonText.text = "Wait & Memorize...";
            maxEachSequence = codeSequenceSecondPart.Length;
            foreach (int i in codeSequenceSecondPart)
            {
                IterationBlock(i, true);
                yield return new WaitForSeconds(waitTime);
                IterationBlock(i, false);
                yield return new WaitForSeconds(waitTime);
            }

            thirdSequence = true;
            isPlayerTurn = true;
            startButtonText.text = "Enter Sequence & Start Next";
        }

        if (thirdSequence && !secondSequence && !firstSequence)
        {
            startButtonText.text = "Wait & Memorize...";
            maxEachSequence = codeSequenceThirdPart.Length;
            foreach (int i in codeSequenceThirdPart)
            {
                IterationBlock(i, true);
                yield return new WaitForSeconds(waitTime);
                IterationBlock(i, false);
                yield return new WaitForSeconds(waitTime);
            }

            thirdSequence = false;
            isPlayerTurn = true;
            startButtonText.text = "Enter Sequence & Get Code!";
        }
    }

    private void IterationBlock(int index, bool sequence)
    {
        if (sequence)
        {
            Color c = new Color32(110, 90, 68, 255);
            alphabet[index].color = c;
            flagSoundEffect.Play();
        }
        else
        {
            Color c = Color.white;
            alphabet[index].color = c;
        }
    }

    private void SetOpacityBasedOnDifficulty(string playerDifficulty)
    {

        int[] easy = new int[] { 1, 6, 7, 8, 10, 11, 12, 15, 19, 22, 23 };
        int[] hard = new int[] { 1, 5, 7, 9, 12, 13, 21, 25 };
        Color c = new Color32(110, 90, 68, 80);

        if(playerDifficulty == "24aea")
        {
            foreach (int index in hard)
            {
                alphabet[index].color = c;
            }
        }
        else
        {
            foreach(int index in easy)
            {
                alphabet[index].color = c;
            }
        }
    }

    public void LetterPressed(int whichLetter)
    {
        if (isPlayerTurn && startIndexForEachSequence < maxEachSequence)
        {
            startIndexForEachSequence++;
            playerText.text += semaphoreFlag[whichLetter];
            playersInputs[currentIndexToUpdatePlayersInputs] = whichLetter;
            currentIndexToUpdatePlayersInputs++;
            flagSoundEffect.Play();
        }

    }

    private int CheckPlayersMistakes()
    {
        int index = 0;
        foreach(int element in codeSequenceFirstPart)
        {

            if(element != playersInputs[index])
            {
                numberOfMistakes++;
            }
            index++;
        }

        foreach (int element in codeSequenceSecondPart)
        {
            if (element != playersInputs[index])
            {
                numberOfMistakes++;
            }
            index++;
        }

        foreach (int element in codeSequenceThirdPart)
        {

            if (element != playersInputs[index])
            {
                numberOfMistakes++;
            }
            index++;
        }

        return StarsCalculation(numberOfMistakes);
    }

    private int StarsCalculation(int numberOfMistakes)
    {
        if(numberOfMistakes < maxMistakeThreshold)
        {
            ReputationStatic.SetReputationForSemaphoreGame(5);
            ReputationStatic.SetSemaphoreGameToTrue();
            return 3;
        }else if(numberOfMistakes >=maxMistakeThreshold && numberOfMistakes < minMistakeThreshold)
        {
            ReputationStatic.SetReputationForSemaphoreGame(2);
            ReputationStatic.SetSemaphoreGameToTrue();
            return 2;
        }
        else
        {
            ReputationStatic.SetReputationForSemaphoreGame(0);
            ReputationStatic.SetSemaphoreGameToTrue();
            return 1;
        }
    }

}
