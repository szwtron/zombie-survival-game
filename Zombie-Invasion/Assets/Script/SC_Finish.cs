using System;
using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class SC_Finish : MonoBehaviour
{
    public GameObject hero;

    public GameObject finishGameUI;

    public bool isGameFinished;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Character>().cursorLocked = !other.GetComponent<Character>().cursorLocked;
            other.GetComponent<Character>().UpdateCursorState();
            Time.timeScale = 0f;
            finishGame();
        }
    }
    
    void finishGame()
    {
        isGameFinished = true;
        
        finishGameUI.SetActive(true);
    }
}
