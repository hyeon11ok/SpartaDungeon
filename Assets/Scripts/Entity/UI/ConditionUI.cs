using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : BaseUI
{
    protected override UIState UIState => UIState.Condition;

    [SerializeField] private Image healthImg;

    private void OnEnable()
    {
        CharacterManager.Instance._Player.Condition.OnHealthChanged += UpdateHealthImg;        
    }

    private void OnDisable()
    {
        CharacterManager.Instance._Player.Condition.OnHealthChanged -= UpdateHealthImg;
    }

    public void UpdateHealthImg(float value)
    {
        healthImg.fillAmount = value;
    }
}
