using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PersistantData : MonoBehaviour
{

    private string[] codes = new string[] { "8aea_EntranceGame", "8bjw_EntranceGame", "abcd_HiddenItems", "dcba_HiddenItems", "aaaa_SemaphoreGame", "bbbb_SemaphoreGame", "cccc_Engine","dddd_Engine", "eeee_Mixing", "ffff_Mixing" };
    public TMP_InputField playerInput;
    public string test;
    public int reputation;

    void Start()
    {
        
    }

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
            Debug.Log(substring[0]);
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
