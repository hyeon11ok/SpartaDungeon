using UnityEngine;

/// <summary>
/// 플레이어와의 충돌이 발생했을 때 호출되는 인터페이스.
/// </summary>
public interface ICollsionEnter
{
    void EnterEvent(GameObject other);
}
