using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class Movement : MonoBehaviour
{
    private InputHandler _input;


    public static float Stamina = 100;
    public static bool Running;

    [SerializeField]
    private bool RotateTowardMouse;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;

    private CharacterAnimation Animation;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();      //ref to input handler script
        Animation = GetComponent<CharacterAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);
        Running = (Running || Stamina > 25) && Input.GetKey(KeyCode.LeftShift) && Stamina > 0 && !Shooting.Reloading;

        

        if (Running)
        {
            Stamina -= Time.deltaTime * 20;
        } else
        {
          Stamina += Time.deltaTime * 10;
          if (Stamina > 100)
          {
            Stamina = 100;
          }
        }
        Animation.SetRunning(Running);

        // if (!RotateTowardMouse)
        // {
        //     RotateTowardMovementVector(movementVector);
        // }
        // if (RotateTowardMouse)
        // {
        //     RotateFromMouseVector();
        // }

  }

    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);    //ray to access where the mouse coordinates are

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;        //won't rotate character in y 
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;
        var targetPosition = transform.position + targetVector.normalized * speed * (Running ? 1.5f : 1);
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if(movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }
}