using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGameController : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
