using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameLogic : MonoBehaviour
{

    int score = 0;
    int health = 0;

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType<GameLogic>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore (int scoreValue)
    {
        score += scoreValue; 
    }
    public void SubtractToHealth(int healthValue)
    {
        health -= healthValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
