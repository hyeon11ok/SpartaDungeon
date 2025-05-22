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

    [SerializeField] private Condition health;

    private void Start()
    {
        health.Init();
        OnHealthChanged?.Invoke(health.GetValuePer());
    }

    public void Heal(float amount)
    {
        health.Increase(amount);
        OnHealthChanged?.Invoke(health.GetValuePer());
    }

    public void TakeDamage(float amount)
    {
        health.Decrease(amount);
        OnHealthChanged?.Invoke(health.GetValuePer());
        if(health.CurValue <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
    }
}
