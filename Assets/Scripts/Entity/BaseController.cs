using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody _rigidbody;

    [Header("Moving")]
    [SerializeField] protected float moveSpeed;
    protected Vector2 moveDir;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
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
}
