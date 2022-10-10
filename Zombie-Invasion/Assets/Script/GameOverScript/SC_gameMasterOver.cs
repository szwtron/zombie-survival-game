using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_gameMasterOver : MonoBehaviour
{
    public static bool isGameOver;

    public GameObject gameOverUI;

    private SC_AIController sc_aiController;
    // public GameObject zombie;

    private void Start()
    {
        // sc_aiController = zombie.GetComponent<SC_AIController>();
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        // if (sc_aiController.m_CaughtPlayer)
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
