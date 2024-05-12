using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timerTarget = 180f;
    [SerializeField]
    private float coinThreshold = 5; 

    private int coinCounter;

    private bool endGame;

    [SerializeField]
    private TMP_Text _uiTimer;

    [SerializeField]
    private TMP_Text _gameOverText;

    [SerializeField]
    private TeamScoreCounter _team1Score, _team2Score;

    [SerializeField]
    private GameObject[] _players;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerTarget -= Time.deltaTime;
        SetUiTimer();


        if(timerTarget < 0 | coinThreshold <= coinCounter )
        {
            _gameOverText.gameObject.SetActive(true);
            
            if (_team1Score.TeamScore == _team2Score.TeamScore)
            {
                _gameOverText.text = "Game Over!\nIt's a draw!";
            }
            else if (_team1Score.TeamScore > _team2Score.TeamScore)
            {
                _gameOverText.text = "Game Over!\nTeam 1 wins!";
            }
            else
            {
                _gameOverText.text = "Game Over!\nTeam 2 Wins!";
            }

            foreach (GameObject player in _players)
            {
                player.SetActive(false);
                timerTarget = 0f;
                this.enabled = false;
            }

            endGame = true;
        }

    }

    private void SetUiTimer()
    {
        int minutes = (int)timerTarget / 60;
        int seconds = (int)timerTarget % 60;

        _uiTimer.text = minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0');
    }
}
