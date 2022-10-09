using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_gameMasterOver : MonoBehaviour
{
    public static bool isGameOver;
    
    public GameObject cube;

    public GameObject gameOverUI;

    private void Start()
    {
        isGameOver = false;
        cube = GameObject.Find("dummy object");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if (cube.transform.position.y >= 5)
        {
            endGame();
        }
    }

    void endGame()
    {
        isGameOver = true;
        
        gameOverUI.SetActive(true);
    }
}
