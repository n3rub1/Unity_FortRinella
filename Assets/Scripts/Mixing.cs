using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Mixing : MonoBehaviour
{
    private int totalMix = 0;
    private int totalNumberOfMix = 5;
    private bool isGameOver = false;
    private bool starsInScreen = false;
    public TMP_Text totalMixtureText;
    public StarsMoveToScreen starsMoveToScreen;


    // Update is called once per frame
    void Update()
    {
        if(totalNumberOfMix == 0 && totalMix == 100 && !isGameOver && !starsInScreen)
        {
            isGameOver = true;
        }

        if(totalNumberOfMix == 0 && totalMix != 100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isGameOver)
        {
            starsInScreen = true;
            isGameOver = false;
            FinishGame();
        }
    }

    public void AddToTotalMixture(int mix)
    {
        totalMix += mix;
        totalNumberOfMix--;
        totalMixtureText.text = "Total Mix: " + totalMix + "/100";
    }

    private void FinishGame()
    {
        starsMoveToScreen.CycleItemClick(3, "No Code for this Game");
    }
}
