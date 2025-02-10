using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1.0f;
    private Vector2 inputAxis;
    private CharacterController characterController;
    public InputActionReference  action;
    public Transform playerCamera;

    void Start()
    {   
        action.action.Enable();
        characterController = GetComponent<CharacterController>();
        action.action.performed += ctx => {};
        action.action.canceled += ctx => {};
    }

    void OnDestroy()
    {
        action.action.performed -= ctx => {};
        action.action.canceled -= ctx => {};
    }

    // Update is called once per frame
    void Update()
    {   

        Vector2 inputAxis = action.action.ReadValue<Vector2>();
        Debug.Log("inputAxis: " + inputAxis);

        // Define movement based on look
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;

        // Movement is only happening in x axis
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * inputAxis.y + right * inputAxis.x;
        characterController.Move(move * Time.deltaTime * speed);
    }

}
