using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    public AudioSource backgroundMusic;
    private string battleShipScene = "Battleship";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == battleShipScene && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
        else if (scene.name != battleShipScene && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    // Optional: If you want to unsubscribe from the event when the object is destroyed.
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
