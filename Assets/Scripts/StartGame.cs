using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    private const string SCREEN_TO_OPEN = "PlayScene";

    public void LaunchPlayScreen()
    {
        SceneManager.LoadScene(SCREEN_TO_OPEN);
    }
}
