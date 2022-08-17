using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStickMovement : MonoBehaviour
{
    public float speed = 10f;
    public FloatingJoystick floatingJoystick;
    public Vector3 sizeChange;
    public Vector3 sizeMinusChange;
    private SizeChange sizeChangee;
    float AIsize;

    private bool obstacle = false;

    private Vector3 moveDirection; 
    Vector3 moveAmount;
    Vector3 smoothing;
    Rigidbody rb;
    public float size = 10;
    public float sizeChanger = 0.0005f;
    public float sizeMinusChanger = -0.005f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        floatingJoystick = FindObjectOfType<FloatingJoystick>();
        sizeChangee = GameObject.Find("Enemy").GetComponent<SizeChange>();
    }

    void Update()
    {
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveDirection =Vector3.right * floatingJoystick.Horizontal +
                        Vector3.forward * floatingJoystick.Vertical;
        Vector3 targetMoveAmount = moveDirection * speed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothing, 0.1f);
        
        AIsize = sizeChangee.size;

    }
// size and speed changes for the player
    private void FixedUpdate() {
        if(moveAmount == new Vector3(0,0,0)) return;
        else if(!obstacle)
        {
           transform.localScale  += sizeChange;
           size += sizeChanger;
           speed -= 0.0005f;
        }
        else if (obstacle)
        {
            transform.localScale -= sizeMinusChange;
            size -= sizeMinusChanger;
        }
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if(SizeChange.isEaten == true){
            size += AIsize;
            transform.localScale = new Vector3(size, size, size);
            speed -= AIsize/1000;
            SizeChange.isEaten = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            obstacle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            obstacle = false;
        }
    }
}