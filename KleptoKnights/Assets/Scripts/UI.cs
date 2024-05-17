using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider HealthBar;
    public TMP_Text ScoreField;
    private GameObject TeamScoreBox;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = GetComponent<EnemyHealth>().maxHealth;
        HealthBar.value = HealthBar.maxValue;


    }
}
