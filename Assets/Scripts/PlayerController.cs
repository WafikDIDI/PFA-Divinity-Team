﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSmoothTime = 0.1f;
    public CharacterController characterController;

    [SerializeField] private float moveSpeed = 6f;

    private float turnSmoothVelocity;
    private AnimationStates animationState;
    private float currentSpeed=0f;
    private Vector3 lookPos;

    private void Start()
    {
        animationState = GetComponent<AnimationStates>();
        animationState.ChangeAnimationState(0);
    }

    void Update()
    {
        RotationAndCursor();
        Move();
        //AttackShoot();
    }


    private void RotateTowardMousePosition()
    {
         Vector2 positionOnscreen = Camera.main.WorldToViewportPoint(transform.position);
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

         var angle = AngleBetweenPoints(positionOnscreen, mouseOnScreen);
         transform.rotation = Quaternion.Euler(new Vector3(0f, angle,0f));
    }

    private float AngleBetweenPoints(Vector3 a,Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void RotateRaycastWithPlan()
    {
        Ray camerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(camerRay,out hit,100))
        {
            lookPos = hit.point;
            Debug.DrawLine(camerRay.origin, lookPos, Color.blue);
        }

        Vector3 lookDirection = lookPos - transform.position;
        lookDirection.y = 0f;

        transform.LookAt(transform.position + lookDirection, Vector3.up);
        //transform.Rotate(transform.position + lookDirection);
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        ChangeAnimation(0);

        if (direction.magnitude >= 0.1f)
        {
            if (!Input.GetMouseButton(1))
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

            }

                characterController.Move(direction * moveSpeed * Time.deltaTime);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    characterController.Move(direction * moveSpeed *2* Time.deltaTime);
                    currentSpeed = moveSpeed * 2;
                }
                else
                {
                    currentSpeed = moveSpeed;
                }
                ChangeAnimation(currentSpeed);

            if (Input.GetMouseButton(1) == false)
            {
            }
            else
            {
                //characterController.Move(Vector3.zero);
            }
        }
    }

    private void ChangeAnimation(float value)=> animationState.ChangeAnimationState(value);
    
    private void RotationAndCursor()
    {
        if (Input.GetMouseButton(1))
        {
            RotateRaycastWithPlan();
            //RotateTowardMousePosition();
            //GameManager.instance.ChangeCursor(1);
        }
        else
        {
          // GameManager.instance.ResetCursor();
        }
    }

    public Vector3 GetMouPotion()
    {
        Ray camerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camerRay, out hit, 100))
        {
            lookPos = hit.point;
            Debug.DrawLine(camerRay.origin, lookPos, Color.blue);
        }

        Vector3 lookDirection =  transform.position;
        lookDirection.y = 0f;

        return lookDirection;
    }

    
}