using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudioStandAlone : MonoBehaviour
{
    public AudioSource buttonClick;

    public void PlayOnClick()
    {
        buttonClick.Play();
    }

}
