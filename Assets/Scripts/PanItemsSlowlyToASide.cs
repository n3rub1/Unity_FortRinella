using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanItemsSlowlyToASide : MonoBehaviour
{
    public float speed;
    public int maxX;
    public int maxY;
    public bool controlledByButton;
    private bool textEnabled = false;


    private void Update()
    {
        if (textEnabled || !controlledByButton)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(maxX, -maxY, 0);
        }
    }

    public void ChangeAppearance()
    {
        textEnabled = !textEnabled;
    }

}
