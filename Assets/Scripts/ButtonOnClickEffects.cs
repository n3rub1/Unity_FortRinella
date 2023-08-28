using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnClickEffects : MonoBehaviour
{
    public GameObject buttonClickEffect;
    public Vector3 buttonLocation;

    public void PlayEffectOnClick()
    {
        Instantiate(buttonClickEffect, buttonLocation, Quaternion.identity);
    }

}