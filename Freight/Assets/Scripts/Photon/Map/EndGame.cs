﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EndGame : MonoBehaviour
{
    private HashSet<Collider> colliders = new HashSet<Collider>();

    public event Action StartEndGame;
    public event Action EndTheGame;
    private bool gameEnding;
    private float timeToEnd;
    private bool gameWon;
    private bool showingEndScreen;
    private float endScreen;

    public HashSet<Collider> GetColliders() { return colliders; }

    void Start()
    {
        // subscribe event to function
        StartEndGame += HandleEndGame;
        EndTheGame += ShowEndScreen;
        gameEnding = false;
        
    }

    private void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "locomotive")
        {
            colliders.Add(other);
            Debug.Log(other.gameObject);
            Debug.Log(colliders.Count);
            // 6 box colliders on the train so when all of them are in endgame, start endgame
            if (colliders.Count == 6)
            {
                StartEndGame();
            }
        }
        
    }

    private void OnTriggerExit (Collider other)
    {
        colliders.Remove(other);
        Debug.Log(other.gameObject);
    }

    private void HandleEndGame()
    {
        gameEnding = true;
        timeToEnd = 0f;
    }

    private void ShowEndScreen()
    {
        endScreen = 0f;
        showingEndScreen = true;
    }

    void Update()
    {
        if (gameEnding)
        {
            timeToEnd += Time.deltaTime;
            if (timeToEnd > 5f)
            {
                gameWon = true;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in players)
                {
                    if (!player.GetComponent<PlayerMovementPhoton>().OnTrain)
                    {
                        gameWon = false;
                        break;
                    }
                }
                gameEnding = false;

                if (gameWon == true)
                {
                    Debug.Log("you won!");
                    foreach (var player in players)
                    {
                        player.transform.GetChild(13).GetChild(0).gameObject.SetActive(true);
                    }
                } 
                else
                {
                    Debug.Log("you lost...");
                    foreach (var player in players)
                    {
                        player.transform.GetChild(13).GetChild(1).gameObject.SetActive(true);
                    }
                }

                EndTheGame();
                
            }
        }
        else if (showingEndScreen)
        {
            endScreen += Time.deltaTime;
            if (endScreen > 3f)
            {
                PhotonNetwork.LoadLevel(0);
                showingEndScreen = false;
            }
        }
    }
}
