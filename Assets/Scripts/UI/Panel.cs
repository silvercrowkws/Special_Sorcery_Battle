using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    Button exitButton;

    Button gameStartButton;

    private void Awake()
    {
        exitButton = transform.GetChild(0).GetComponent<Button>();
        exitButton.onClick.AddListener(Exit);

        gameStartButton = transform.GetChild(1).GetComponent<Button>();
        gameStartButton.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    private void Exit()
    {
        Application.Quit();

    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #endif
    }
}
