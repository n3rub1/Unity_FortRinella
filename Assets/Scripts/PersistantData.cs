using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PersistantData : MonoBehaviour
{

    private string[] codes = new string[] { "8aea_EntranceGame", "8bjw_EntranceGame", "18aea_HiddenItems", "18bjw_HiddenItems", "24aea_SemaphoreGame", "24bjw_SemaphoreGame", "28aea_Engine","28bjw_Engine", "32aea_Mixing", "32bjw_Mixing", "37aea_Battleship", "37bjw_Battleship" };
    public TMP_InputField playerInput;
    public string test;
    public int reputation;

    // Update is called once per frame
    void Update()
    {
        test = playerInput.text;   
    }

    public void NextButton()
    {
        bool isOk = false;

        foreach(string code in codes)
        {
            string[] substring = code.Split('_');
            if(playerInput.text == substring[0])
            {
                isOk = true;
                PlayerPrefs.SetString("code", substring[0]);
                SceneManager.LoadScene(substring[1]);
            }
        }

        if (!isOk)
        {
            playerInput.text = "Incorrect";
        }

    }


}
