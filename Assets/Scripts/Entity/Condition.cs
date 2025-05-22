using System;
using UnityEngine;

public enum ConditionType
{
    Health,
    Stamina
}

[Serializable]
public class Condition
{
    [SerializeField] private float maxValue;
    public float CurValue { get; private set; }
    [SerializeField] private float startValue;
    [SerializeField] private ConditionType conditionType;
    [SerializeField] private float passiveValue; // 자동 회복/감소량
    [SerializeField] private bool isPassive; // 자동 회복/감소 여부

    public ConditionType ConditionType => conditionType;
    public bool IsPassive => isPassive;

    public void Init()
    {
        CurValue = startValue;
    }

    public void Passive()
    {
        CurValue += passiveValue * Time.deltaTime;

        if(CurValue > maxValue)
            CurValue = maxValue;
        else if(CurValue < 0)
            CurValue = 0;
    }

    /// <summary>
    /// 현재 값을 증가시킵니다.
    /// </summary>
    /// <param name="value">증가값</param>
    public void Increase(float value)
    {
        CurValue += value;
        if(CurValue > maxValue)
            CurValue = maxValue;
    }

    /// <summary>
    /// 현재 값을 감소시킵니다.
    /// </summary>
    /// <param name="value">감소값</param>
    public void Decrease(float value)
    {
        CurValue -= value;
        if(CurValue < 0)
            CurValue = 0;
    }

    public float GetValuePer()
    {
        return CurValue / maxValue;
    }
}
