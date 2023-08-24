using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHP;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealhBar;
    private void Start()
    {
        totalHealthBar.fillAmount = playerHP.CurrentHP / 10;
    }

    // Update is called once per frame
    private void Update()
    {
        currentHealhBar.fillAmount = playerHP.CurrentHP / 10;
    }
}
