using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] private List<Condition> conditionsList = new List<Condition>();
    private Condition[] passiveConditions; // 값이 자동으로 증감하는 Condition들

    public Condition Health => GetCondition(ConditionType.Health);
    public Condition Stamina => GetCondition(ConditionType.Stamina);

    private void Start()
    {
        foreach(Condition con in conditionsList)
        {
            con.Init();
        }

        passiveConditions = GetConditions(true);
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
    public Condition GetCondition(ConditionType conditionType)
    {
        return conditionsList.Find((x) => x.ConditionType == conditionType);
    }

    /// <summary>
    /// 지정된 passive 여부와 일치하는 Condition들을 반환합니다.
    /// </summary>
    /// <param name="isPassive"></param>
    /// <returns></returns>
    public Condition[] GetConditions(bool isPassive)
    {
        return conditionsList.FindAll((x) => x.IsPassive == isPassive).ToArray();
    }

    public void Heal(float amount)
    {
        GetCondition(ConditionType.Health).Increase(amount);
    }

    public void TakeDamage(float amount)
    {
        GetCondition(ConditionType.Health).Decrease(amount);
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
