using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reputation : MonoBehaviour
{
    public TMP_Text reputationText;
    public void UpdateReputationAndShow()
    {
        reputationText.text = "Total Reputation Gained: " + ReputationStatic.GetReputation().ToString();
    }
}
