using UnityEngine;

/// <summary>
/// 플레이어와의 충돌이 끝나면 호출되는 인터페이스.
/// </summary>
public interface ICollsionExit
{
    void ExitEvent(GameObject other);
}
