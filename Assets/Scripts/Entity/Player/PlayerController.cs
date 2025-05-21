using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 이동 및 회전 스크립트
/// </summary>
public class PlayerController:MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("Moving")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveDir;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpRate; // 점프 쿨타임
    private float lastJumpTime = 0; // 마지막 점프 시간
    private bool isJumpingInput = false; // 점프 입력 여부

    [Header("Look")]
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float minXRot;
    [SerializeField] private float maxXRot;
    private float curXRot; // 현재 카메라 X축 회전 각도
    [SerializeField] [Range(0.1f, 1.0f)] private float mouseSensitivity; // 마우스 감도
    private Vector2 mouseDelta; // 마우스 이동량



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        if(isJumpingInput)
        {
            Jump();
        }
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    public void SetDir(Vector2 dir)
    {
        moveDir = dir;
    }

    private void Jump()
    {
        // 점프 쿨타임이 지나고 바닥에 닿아있을 때 점프
        if(isGrounded() && Time.time - lastJumpTime > jumpRate)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;
        }
    }

    public void IsInputJumpingAction(bool value)
    {
        isJumpingInput = value;
    }

    private bool isGrounded()
    {
        Ray[] rays = new Ray[4];
        rays[0] = new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        rays[1] = new Ray(transform.position - (transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        rays[2] = new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        rays[3] = new Ray(transform.position - (transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down);

        for(int i = 0; i < rays.Length; i++)
        {
            if(Physics.Raycast(rays[i], out RaycastHit hit, 0.3f, groundLayer))
            {
                return true;
            }
        }

        return false;
    }

    private void Look()
    {
        // x축 기준으로 카메라 회전 == 상하 회전
        curXRot += mouseDelta.y * mouseSensitivity * 0.1f;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
        cameraContainer.localEulerAngles = new Vector3(-curXRot, 0, 0);

        // y축 기준으로 플레이어 회전 == 좌우 회전
        transform.localEulerAngles += new Vector3(0, mouseDelta.x * mouseSensitivity * 0.1f, 0);
    }

    public void SetLookDelta(Vector2 delta)
    {
        mouseDelta = delta;
    }
}
