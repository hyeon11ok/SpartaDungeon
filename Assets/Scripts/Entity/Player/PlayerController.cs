using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 시점 타입을 정의합니다.
/// </summary>
public enum ViewType
{
    FirstPerson,
    ThirdPerson
}

/// <summary>
/// 플레이어 이동 및 회전 스크립트
/// </summary>
public class PlayerController:BaseController
{
    [SerializeField] private float moveStamina; // 이동 속도
    [SerializeField] private float runSpeed;
    private bool isRun; // 달리기 여부

    [Header("Jumping")]
    [SerializeField] private float jumpStamina;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpRate; // 점프 쿨타임
    private float lastJumpTime = 0; // 마지막 점프 시간
    private bool isJumpingInput = false; // 점프 입력 여부

    [Header("Look")]
    [SerializeField] private Transform cameraContainer; // 카메라의 회전축
    [SerializeField] private Transform cam; // 카메라 위치
    [SerializeField] private float minXRot;
    [SerializeField] private float maxXRot;
    private float curXRot; // 현재 카메라 X축 회전 각도
    [SerializeField] [Range(0.1f, 1.0f)] private float mouseSensitivity; // 마우스 감도
    private Vector2 mouseDelta; // 마우스 이동량

    [Header("Climb")]
    [SerializeField] private float climbSpeed; // 등반 속도
    [SerializeField] private float rayHeight; // 벽 체크 레이 높이
    [SerializeField] private float rayWidth; // 벽 체크 레이 너비
    [SerializeField] private LayerMask climbLayer; // 등반 가능한 레이어
    [SerializeField] private float climbStamina; // 등반 시 소모되는 스태미너

    [Header("Camera")]
    [SerializeField] private Vector3 fpsPivot;
    [SerializeField] private Vector3 tpsPivot;
    [SerializeField] private Vector3 tpsRot;
    [SerializeField] private ViewType viewType; // 시점 타입
    [SerializeField] private float tpsTargetHeight;
    [SerializeField] private float switchingSpeed; // 카메라 전환 속도
    private IEnumerator camSwitchCoroutine;


    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
    }

    private void Start()
    {
        StartCoroutine(MoveCamera());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(isJumpingInput)
        {
            Jump();
        }
    }

    private void LateUpdate()
    {
        Look();
    }

    protected override void Move()
    {
        base.Move();
        if(isRun)
        {
            CharacterManager.Instance._Player.Condition.UseStamina(moveStamina * Time.deltaTime); // 이동 시 스태미너 감소
        }
        Climb();
    }

    /// <summary>
    /// 달리기 모드 전환
    /// </summary>
    /// <param name="isRun">달리기 여부</param>
    public void SwitchRunMode(bool isRun)
    {
        if(!CharacterManager.Instance._Player.Condition.CanUseStamina(moveStamina * Time.deltaTime))
        {
            isRun = false; // 스태미너가 부족하면 달리기 모드 해제
            return;
        }

        if(isRun)
        {
            moveSpeed += runSpeed;
            this.isRun = true;
        }
        else
        {
            moveSpeed -= runSpeed;
            this.isRun = false;
        }
    }

    private void Jump()
    {
        // 점프 쿨타임이 지나고 바닥에 닿아있을 때 점프
        if(isGrounded() && Time.time - lastJumpTime > jumpRate && CharacterManager.Instance._Player.Condition.CanUseStamina(jumpStamina))
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;
            CharacterManager.Instance._Player.Condition.UseStamina(jumpStamina); // 점프 시 스태미너 감소
        }
    }

    private void Climb()
    {
        // 벽에 붙어있고 앞으로 이동할 때 벽을 기어오르는 동작
        if(IsStickToWall() && moveDir.y > 0 && CharacterManager.Instance._Player.Condition.CanUseStamina(climbStamina * Time.deltaTime))
        {
            Debug.Log("Climbing");
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
            _rigidbody.AddForce(Vector3.up * climbSpeed, ForceMode.VelocityChange);
            CharacterManager.Instance._Player.Condition.UseStamina(climbStamina * Time.deltaTime);
        }
    }

    private bool IsStickToWall()
    {
        Ray[] rays = new Ray[5];
        rays[0] = new Ray(transform.position + (Vector3.up * rayHeight), Vector3.forward); // 상
        rays[1] = new Ray(transform.position, Vector3.forward); // 하
        rays[2] = new Ray(transform.position - (transform.right * rayWidth) + (Vector3.up * rayHeight / 2), Vector3.forward); // 좌
        rays[3] = new Ray(transform.position + (transform.right * rayWidth) + (Vector3.up * rayHeight / 2), Vector3.forward); // 우
        rays[4] = new Ray(transform.position + (Vector3.up * rayHeight / 2), Vector3.forward); // 중앙

        for(int i = 0; i < rays.Length; i++)
        {
            if(Physics.Raycast(rays[i], out RaycastHit hit, 1f, climbLayer))
            {
                return true;
            }
        }
        return false;
    }

    public void IsInputJumpingAction(bool value)
    {
        isJumpingInput = value;
    }

    private bool isGrounded()
    {
        Ray[] rays = new Ray[5];
        rays[0] = new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        rays[1] = new Ray(transform.position - (transform.forward * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        rays[2] = new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        rays[3] = new Ray(transform.position - (transform.right * 0.2f) + (Vector3.up * 0.1f), Vector3.down);
        rays[3] = new Ray(transform.position + (Vector3.up * 0.1f), Vector3.down);

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

    public void SwitchCameraPosition()
    {
        if(viewType == ViewType.FirstPerson)
        {
            viewType = ViewType.ThirdPerson;
        }
        else
        {
            viewType = ViewType.FirstPerson;
        }

        if(camSwitchCoroutine != null)
        {
            StopCoroutine(camSwitchCoroutine);
        }

        camSwitchCoroutine = MoveCamera();
        StartCoroutine(camSwitchCoroutine);
    }

    private IEnumerator MoveCamera()
    {
        Vector3 targetPos;
        Vector3 targetRot;

        if(viewType == ViewType.FirstPerson)
        {
            targetPos = fpsPivot;
            targetRot = Vector3.zero;
        }
        else
        {
            targetPos = tpsPivot;
            targetRot = tpsRot;
        }

        while(Vector3.Distance(cam.localPosition, targetPos) > 0.1f && Vector3.Distance(cam.eulerAngles, targetRot) > 0.1f)
        {
            cam.localPosition = Vector3.Lerp(cam.localPosition, targetPos, Time.deltaTime * switchingSpeed);
            cam.localEulerAngles = Vector3.Lerp(cam.localEulerAngles, targetRot, Time.deltaTime * switchingSpeed);
            yield return null;
        }

        cam.localPosition = targetPos;
        cam.localEulerAngles = targetRot;
    }
}
