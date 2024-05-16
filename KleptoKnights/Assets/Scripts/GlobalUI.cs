using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private int _scoreTeam1, _scoreTeam2;

    private void SetUiScore()
    {
        int teamScoreSum = _scoreTeam1 + _scoreTeam2;

        if (teamScoreSum > 0)
        {
            _slider.maxValue = _scoreTeam1 + _scoreTeam2;
            _slider.value = _scoreTeam1;
        }
        else
        {
            _slider.maxValue = 2;
            _slider.value = 1;
        }
    }

    public void SetScore(GameObject teamScoreCounter)
    {
        if (teamScoreCounter.name == "Team 1")
        {
            _scoreTeam1 = teamScoreCounter.GetComponent<TeamScoreCounter>().TeamScore;
        }
        else
        {
            _scoreTeam2 = teamScoreCounter.GetComponent<TeamScoreCounter>().TeamScore;
        }

        SetUiScore();
    }
}
