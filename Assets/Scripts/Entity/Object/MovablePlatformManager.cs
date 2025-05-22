using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovablePlatformManager : MonoBehaviour
{
    [SerializeField] private MovablePlatform platform;
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private bool isMoveForward = true;
    [SerializeField] private float targetChangeDelay;

    // Start is called before the first frame update
    void Start()
    {
        platform.transform.position = waypoints[0].position;
        platform.SetTarget(waypoints[1].transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(platform.transform.position, waypoints[currentWaypointIndex].position) < 0.2f)
        {
            if(isMoveForward)
            {
                currentWaypointIndex++;
                // 증가시킨 인덱스가 마지막 인덱스에 도달했는지 확인
                if(currentWaypointIndex == waypoints.Length)
                {
                    isMoveForward = false;
                    currentWaypointIndex = waypoints.Length - 2; // 마지막 인덱스 설정
                }
            }
            else
            {
                currentWaypointIndex--;
                // 감소시킨 인덱스가 첫 번째 인덱스에 도달했는지 확인
                if(currentWaypointIndex < 0)
                {
                    isMoveForward = true;
                    currentWaypointIndex = 1; // 첫 번째 인덱스 설정
                }
            }

            Invoke("SetTargetInvoke", targetChangeDelay);
        }
    }

    private void SetTargetInvoke()
    {
        platform.SetTarget(waypoints[currentWaypointIndex].transform);
    }
}
