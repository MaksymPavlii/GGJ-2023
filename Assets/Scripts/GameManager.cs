﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isPlaying;

    public static GameManager instance;
    public GameMode gameMode = GameMode.StoryMode;
    public NetworkMode networkMode = NetworkMode.Local;

    public float requiredRootsAmount;
    private int collectedRootsAmount;

    public static UnityAction OnCollectedCounterUpdated;
    public static UnityAction OnWin;

    private void Awake()
    {
        isPlaying = true;

        if (instance == null)
        { 
            instance = this; 
        }

        else if (instance == this)
        { 
            Destroy(gameObject); 
        }
    }

    public void UpdateRootCounter()
    {
        if (collectedRootsAmount < requiredRootsAmount)
        {
            OnCollectedCounterUpdated?.Invoke();
        }
        collectedRootsAmount++;
        if (collectedRootsAmount >= requiredRootsAmount)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        isPlaying = false;
        OnWin?.Invoke();
        /*Time.timeScale = 0;*/
    }

    public void LoseGame()
    {
        isPlaying = false;
        /*Time.timeScale = 0;*/
    }

    private void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }

    private void SetNetworkMode(NetworkMode networkMode)
    {
        this.networkMode = networkMode;
    }

    public enum GameMode
    {
        StoryMode,
        Versus
    }
    public enum NetworkMode
    {
        Local,
        Multiplayer
    }
}
