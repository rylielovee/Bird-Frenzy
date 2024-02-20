using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  
// added ^

public class InputController : MonoBehaviour
{
    public Vector3 inputDirection = Vector3.zero;

    [SerializeField]
    MovementController movementController;   // reference to MovementController


    public void OnMove(InputAction.CallbackContext context)  // the parameter is a container that holds almost
    {                                                         // anything inside of it
        inputDirection = context.ReadValue<Vector2>();

        movementController.Direction = inputDirection;
    }



}
