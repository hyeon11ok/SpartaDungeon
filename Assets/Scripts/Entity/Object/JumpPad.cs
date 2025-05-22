using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, ICollsionEnter
{
    [SerializeField] private float jumpForce;

    public void EnterEvent(GameObject other)
    {
        if(other.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            Debug.Log("JumpPad Enter");
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); 
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
