using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider HealthBar;
    public Slider WeightBar;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = GetComponent<EnemyHealth>().maxHealth;
        HealthBar.value = HealthBar.maxValue;
    }

    public void UpdateWeightBar(int heldWeight, int weightLimit)
    {
        WeightBar.value = heldWeight;
        WeightBar.maxValue = weightLimit;
    }
}
