using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStickMovement : MonoBehaviour
{
    public float speed = 10f;
    public FloatingJoystick floatingJoystick;
    public Vector3 sizeChange;

    private Vector3 moveDirection; 
    Vector3 moveAmount;
    Vector3 smoothing;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        floatingJoystick = FindObjectOfType<FloatingJoystick>();
        
    }

    void Update()
    {
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveDirection =Vector3.right * floatingJoystick.Horizontal +
                        Vector3.forward * floatingJoystick.Vertical;
        Vector3 targetMoveAmount = moveDirection * speed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothing, 0.1f);
        
    }

    private void FixedUpdate() {
        if(moveAmount == new Vector3(0,0,0)) return;
        else
        {
           transform.localScale  += sizeChange;
           speed -= 0.0005f;
        }
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}