using System;
using UnityEngine;

[Serializable]
public class Condition
{
    [SerializeField] private float maxValue;
    public float CurValue { get; private set; }
    [SerializeField] private float startValue;

    public void Init()
    {
        CurValue = startValue;
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
