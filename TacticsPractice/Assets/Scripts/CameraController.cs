using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Follow on target's X/Z plane
    //smooth rotations around target in 45 degree increments
    public GameObject target;
    public Vector3 offsetPos;
    public float moveSpeed = 5;
    public float turnSpeed = 10;
    public float smoothSpeed = 0.5f;

    Quaternion targetRotation;
    Vector3 targetPos;
    bool smoothRotating = false;

    private void Awake()
    {
        //StartCoroutine("RotateAroundTarget", -45);
    }
    private void Update()
    {
        MoveWithTarget();
        LookAtTarget();

        if(Input.GetKeyDown(KeyCode.G) && !smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", 90);

        }
        if (Input.GetKeyDown(KeyCode.H) && !smoothRotating )
        {
            StartCoroutine("RotateAroundTarget", -90);
        }
        


    }
    public void RotateLeft()
    {
        if (!smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", 90);
        }
    }

    public void RotateRight()
    {
        if (!smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", -90);
        }
    }

    //move the camera to the target position + current camera offset
    //offset is modified by the rotateAround coroutine
    private void MoveWithTarget()
    {
        targetPos = target.transform.position + offsetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

    }


    //use the look vector(target - current) to aim the camera toward the player
    private void LookAtTarget()
    {
        targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        
        


    }

    // This coroutine can only have one instance running at a time
    // Determined by 'smoothRotation'
    IEnumerator RotateAroundTarget(float angle)
    {
        Vector3 vel = Vector3.zero;
        Vector3 targetOffsetPos = Quaternion.Euler(0, angle, 0) * offsetPos;
        float dist = Vector3.Distance(offsetPos, targetOffsetPos);
        smoothRotating = true;

        while(dist > 0.02f)
        {
            offsetPos = Vector3.SmoothDamp(offsetPos, targetOffsetPos, ref vel, smoothSpeed);
            dist = Vector3.Distance(offsetPos, targetOffsetPos);
            yield return null;
        }
        smoothRotating = false;
        offsetPos = targetOffsetPos;
    }

 
    public  void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
    }


}
