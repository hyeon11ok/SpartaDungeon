using System;
using UnityEngine;

public class MovablePlatform : MonoBehaviour, ICollsionEnter, ICollsionExit
{
    private Vector3 targetPos;
    [SerializeField] private float moveSpeed;

    private Transform player;

    private void Update()
    {
        Vector3 pos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // 플레이어가 플랫폼에 탑승했을 때, 플랫폼과 함께 이동
        if(player != null)
        {
            player.position += transform.position - pos;
        }
    }

    public void SetTarget(Transform target)
    {
        targetPos = target.position;
    }

    public void EnterEvent(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

    public void ExitEvent(GameObject other)
    {
        if(player != null && player.gameObject.Equals(other.gameObject))
        {
            player = null;
        }
    }
}
