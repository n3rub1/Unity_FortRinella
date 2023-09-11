using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionsArray : MonoBehaviour
{

    private string[] questions = new string[] {
        "The only entrance to Fort Rinella is through the bent entry and a fortified gate",
        "This fort houses one of the last remaining armstrong 100-ton guns",
        "This fort does not utilize a ditch for extra protection",
        "The bridge can be rolled inside and slides on a metal railing",
        "The gun fired in anger two times", ""
    };

    private string[] questionsHard = new string[]
    {
        "Fort Rinella was built by the British in 1878 to house the Armstrong 100-ton gun",
        "The Armstrong 100-ton gun could fire a shot every 6 minutes",
        "Fort Rinella was actively used in World War II",
        "Fort Rinella was built to protect British shipping against Italian new powerful naval ships",
        "Fort Rinella was designed to protect against BOTH naval and land attacks", ""
    };

    private string[] answersInformationCorrect = new string[] {
        "Correct! The bent entry provided protection against bombardment from land as the gate is hidden",
        "Correct! The last two remaining guns are here and at Gibraltar",
        "Correct! The fort has a dry ditch all along its walls",
        "Correct! The bridge can be rolled inside so anyone attacking will not have easy access to the gate",
        "Correct! The gun never fired in anger"
    };

    private string[] answersHardInformationCorrect = new string[] {
        "Correct! The fort was built in 1878, the gun entered in service in 1884",
        "Correct! The gun could fire once every six minutes",
        "Correct! By World War II, the gun was already obsolete",
        "Correct! The fort was built primarily to protect against the newly built Italian ships",
        "Correct! The fort was designed for naval defence only"
    };

    private string[] answersInformationWrong = new string[] {
        "Wrong! The bent entry provided protection against bombardment from land as the gate is hidden",
        "Wrong! This fort houses one of the last remaining guns, the other one is installed at Gibraltar",
        "Wrong! The fort has a dry ditch all along its walls",
        "Wrong! The bridge can be rolled inside so anyone attacking will not have easy access to the gate",
        "Wrong! The gun never fired in anger"
    };

    private string[] answersHardInformationWrong = new string[] {
        "Wrong! The fort was built in 1878, the gun entered in service in 1884 (the date above the gate)",
        "Wrong! The gun could fire once every six minutes, overcoming any ship firing rate at the time",
        "Wrong! By World War II, the gun was already obsolete by more powerful and higher rate of fire",
        "Wrong! The fort was built primarily to protect against the newly built Italian ships",
        "Wrong! Whilst the fort house some infantry defence, it was primarily designed for naval defence"
    };

    private bool[] answers = new bool[] { true, true, false, true, false };
    public TMP_Text questionText;
    public TMP_Text answerText;
    public AudioSource correctSoundEffect;
    public AudioSource wrongSoundEffect;
    public AudioSource nextButtonSoundEffect;

    TMP_Text nextButtonText;
    public Button nextButton;
    public Button trueButton;
    public Button falseButton;
    private bool gameReady = false;
    private bool exit = false;
    private int currentIndex = 0;
    private string code;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        code = PlayerPrefs.GetString("code");
        nextButton.interactable = false;
        trueButton.interactable = true;
        falseButton.interactable = true;
        nextButtonText = nextButton.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (code == "8aea" && !exit)
        {
            questionText.text = questionsHard[currentIndex];
        }
        else if (code == "8bjw" && !exit)
        {
            questionText.text = questions[currentIndex];
        }

        if (currentIndex == 5)
        {
            nextButton.interactable = true;
            nextButtonText.text = "Exit";
            exit = true;
        }
    }

    public void UpdateIndex()
    {
        nextButtonSoundEffect.Play();

        if (currentIndex < 4 && !gameReady)
        {
            currentIndex++;
            nextButton.interactable = false;
            trueButton.interactable = true;
            falseButton.interactable = true;
        }

        if (gameReady)
        {
            currentIndex = 5;
            if (score == 5)
            {
                questions[5] = "3241";
                questionsHard[5] = "3241";
                nextButton.interactable = false;

                if (code == "8aea")
                {
                    ReputationStatic.SetReputationForEntranceGame(3);
                    ReputationStatic.SetEntranceGameToTrue();
                }

            }
            else
            {
                questions[5] = "1243";
                questionsHard[5] = "1243";
                nextButton.interactable = false;
            }
        }

        if (exit)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void AnswerSelect(bool answer)
    {

        if (answer == answers[currentIndex])
        {
            if (code == "8aea")
            {
                answerText.text = answersHardInformationCorrect[currentIndex];
                correctSoundEffect.Play();
                score++;
            }
            else if (code == "8bjw")
            {
                answerText.text = answersInformationCorrect[currentIndex];
                correctSoundEffect.Play();
                score++;
            }

        }
        else
        {
            if (code == "8aea")
            {
                answerText.text = answersHardInformationWrong[currentIndex];
                wrongSoundEffect.Play();
            }
            else if (code == "8bjw")
            {
                answerText.text = answersInformationWrong[currentIndex];
                wrongSoundEffect.Play();
            }

        }

        if (currentIndex == 4)
        {
            nextButtonText.text = "Code";
            gameReady = true;
            nextButton.interactable = true;
            questionText.text = "";
        }
        else
        {
            nextButton.interactable = true;
        }

        if (currentIndex == 5)
        {
            answerText.text = "";
        }

        trueButton.interactable = false;
        falseButton.interactable = false;
    }


}
