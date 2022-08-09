using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOfPlanett : MonoBehaviour
{
    
    public float gravity = 10f;
    

    
    void Start()
    {
       
    }


    public void Attract(Transform body)
    {
        Vector3 targetDirection = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(body.up, targetDirection) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetDirection * gravity);
    }
}

