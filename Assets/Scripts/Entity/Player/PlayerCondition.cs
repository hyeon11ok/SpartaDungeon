using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    /// <summary>
    /// 플레이어의 체력이 변경될 때 호출되는 이벤트입니다.
    /// </summary>
    public event Action<float> OnHealthChanged;

    [SerializeField] private List<Condition> conditionsList;
    private Condition[] passiveConditions;

    public Condition Health => GetCondition(ConditionType.Health);
    public Condition Stamina => GetCondition(ConditionType.Stamina);

    private void Start()
    {
        foreach(Condition con in conditionsList)
        {
            con.Init();
        }

        passiveConditions = GetCondition(true);
        OnHealthChanged?.Invoke(GetCondition(ConditionType.Health).GetValuePer());
    }

    private void Update()
    {
        for(int i = 0; i < passiveConditions.Length; i++)
        {
            passiveConditions[i].Passive();
        }
    }

    /// <summary>
    /// 지정된 타입과 일치하는 Condition을 반환합니다.
    /// </summary>
    /// <param name="conditionType">목표 타입</param>
    /// <returns></returns>
    private Condition GetCondition(ConditionType conditionType)
    {
        return conditionsList.Find((x) => x.ConditionType == conditionType);
    }

    /// <summary>
    /// 지정된 passive 여부와 일치하는 Condition들을 반환합니다.
    /// </summary>
    /// <param name="isPassive"></param>
    /// <returns></returns>
    private Condition[] GetCondition(bool isPassive)
    {
        return conditionsList.FindAll((x) => x.IsPassive == isPassive).ToArray();
    }

    public void Heal(float amount)
    {
        GetCondition(ConditionType.Health).Increase(amount);
        OnHealthChanged?.Invoke(GetCondition(ConditionType.Health).GetValuePer());
    }

    public void TakeDamage(float amount)
    {
        GetCondition(ConditionType.Health).Decrease(amount);
        OnHealthChanged?.Invoke(GetCondition(ConditionType.Health).GetValuePer());
        if(GetCondition(ConditionType.Health).CurValue <= 0)
        {
            Die();
        }
    }

    public void UseStamina(float amount)
    {
        GetCondition(ConditionType.Stamina).Decrease(amount);
    }

    private void Die()
    {
        Debug.Log("Player has died.");
    }
}
