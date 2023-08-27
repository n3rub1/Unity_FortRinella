using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    public void Jump(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
