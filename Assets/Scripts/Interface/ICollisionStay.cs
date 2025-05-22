using UnityEngine;

/// <summary>
/// 플레이어와의 충돌이 유지되는 동안 호출되는 인터페이스.
/// </summary>
public interface ICollsionStay
{
    void StayEvent(GameObject other);
}
