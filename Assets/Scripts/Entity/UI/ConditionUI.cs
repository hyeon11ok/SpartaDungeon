using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : BaseUI
{
    protected override UIState UIState => UIState.Condition;

    [SerializeField] private Image healthImg;

    protected override void Start()
    {
        base.Start();
        PlayerManager.Instance.Player.Condition.OnHealthChanged += UpdateHealthImg;
    }

    public void UpdateHealthImg(float value)
    {
        healthImg.fillAmount = value;
    }
}
