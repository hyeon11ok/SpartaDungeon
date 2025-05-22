using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : BaseUI
{
    protected override UIState UIState => UIState.Condition;

    [SerializeField] private Image healthImg;
    [SerializeField] private Image staminaImg;

    private void OnEnable()
    {
        StartCoroutine(WaitForSubscribe(() => {
            CharacterManager.Instance._Player.Condition.GetCondition(ConditionType.Health).OnValueChanged += UpdateHealthImg;
            CharacterManager.Instance._Player.Condition.GetCondition(ConditionType.Stamina).OnValueChanged += UpdateStaminaImg;
        }));
    }

    private void OnDisable()
    {
        CharacterManager.Instance._Player.Condition.GetCondition(ConditionType.Health).OnValueChanged -= UpdateHealthImg;
        CharacterManager.Instance._Player.Condition.GetCondition(ConditionType.Stamina).OnValueChanged -= UpdateStaminaImg;
    }

    public void UpdateHealthImg(Condition condition)
    {
        healthImg.fillAmount = condition.CurValue / condition.MaxValue;
    }

    public void UpdateStaminaImg(Condition condition)
    {
        staminaImg.fillAmount = condition.CurValue / condition.MaxValue;
    }

    protected IEnumerator WaitForSubscribe(Action action)
    {
        yield return new WaitUntil(() => CharacterManager.IsAwakeDone);
        action();
    }
}
