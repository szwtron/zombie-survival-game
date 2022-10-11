using System;
using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class SC_gameMasterOver : MonoBehaviour
{
    public static bool isGameOver;

    public GameObject gameOverUI;

    // private Character character;
    //
    // public GameObject hero;

    private void Start()
    {
        // character = hero.GetComponent<Character>();
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        // if (character.playerHealth <= 0)
        // {
        //     endGame();
        // }
    }

    void endGame()
    {
        isGameOver = true;
        
        gameOverUI.SetActive(true);
    }
}
