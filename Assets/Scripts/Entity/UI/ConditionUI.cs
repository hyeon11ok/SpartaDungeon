using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : BaseUI
{
    protected override UIState UIState => UIState.Condition;

    [SerializeField] private Image healthImg;
    [SerializeField] private Image staminaImg;
    [SerializeField] private Image hitImg;
    private IEnumerator hitImgCoroutine;

    private void OnEnable()
    {
        StartCoroutine(WaitForSubscribe(() => {
            CharacterManager.Instance._Player.Condition.GetCondition(ConditionType.Health).OnValueChanged += UpdateHealthImg;
            CharacterManager.Instance._Player.Condition.GetCondition(ConditionType.Stamina).OnValueChanged += UpdateStaminaImg;
            CharacterManager.Instance._Player.Condition.OnHit += ActiveHitImag;
        }));
    }

    public void UpdateHealthImg(Condition condition)
    {
        healthImg.fillAmount = condition.CurValue / condition.MaxValue;
    }

    public void UpdateStaminaImg(Condition condition)
    {
        staminaImg.fillAmount = condition.CurValue / condition.MaxValue;
    }

    public void ActiveHitImag()
    {
        Color color = hitImg.color;
        color.a = 0.5f;
        hitImg.color = color;

        if(hitImgCoroutine != null)
            StopCoroutine(hitImgCoroutine);
        StartCoroutine(hitImgCoroutine = HitImageFadeOut());
    }

    private IEnumerator HitImageFadeOut()
    {
        Color color = hitImg.color;
        while(color.a > 0)
        {
            color.a -= Time.deltaTime;
            hitImg.color = color;
            yield return null;
        }
    }
}
