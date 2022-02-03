using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XavHelpTo;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button close;

    private void Awake()
    {
        play.onClick.AddListener(LoadGame);
        close.onClick.AddListener(Close);
    }
    private void Start()
    {
        AudioSystem.PlayMusic(GeneralMusic.MENU_1);
    }
    private void LoadGame() => SceneManager.LoadScene("Game");
    private void Close() => Application.Quit();
}
