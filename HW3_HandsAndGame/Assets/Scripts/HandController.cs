using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode] // Suorittaa koodin myös editorissa ilman pelitilaa
public class HandController : MonoBehaviour
{
    // Animation
    public InputActionReference gripInput;
    public InputActionReference triggerInput;

    private Animator animator;
    private bool wasPressed = false;


    // Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    private Transform _followTarget;

    private Rigidbody _body;




    private void Start()
    {
        // Animation
        animator = GetComponent<Animator>();

        if (Application.isPlaying) // Estetään turhat kutsut editoritilassa
        {
            gripInput.action.Enable();
            triggerInput.action.Enable();
        }

        // Physic Movement
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        //Debug.Log("_body" + _body);
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;

        // Teleport hands
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;

    }

    void Update()
    {
        if (animator == null)
        {
            return;
        }

        if (Application.isPlaying) // PELITILASSA: päivitä inputin mukaan
        {
            bool isPressed = gripInput.action.IsPressed();

            if (isPressed && !wasPressed)
            {
                Debug.Log("Nappia painetaan!");
            }

            wasPressed = isPressed;

            float grip = gripInput.action.ReadValue<float>();
            float trigger = triggerInput.action.ReadValue<float>();

            animator.SetFloat("Grip", grip);
            animator.SetFloat("Trigger", trigger);

            //PhysicsMove();
        }
        else // EDITORITILASSA: Pakota animaatio päälle
        {
            animator.Update(0);
        }
    }

    private void FixedUpdate()
    {
        PhysicsMove();
    }


    private void PhysicsMove()
    {
        if (_body == null || _followTarget == null) return;

        // Position
        Vector3 positionWithOffset = _followTarget.position + positionOffset;
        //Vector3 targetPosition = _followTarget.position;
        _body.linearVelocity = (positionWithOffset - _body.position) * followSpeed;

        // Rotation
        Quaternion rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        //Quaternion targetRotation = _followTarget.rotation;
        Quaternion deltaRotation = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f) angle -= 360f;
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

    }
}

//jäi kohtaan 11.00


// using UnityEngine;
// using UnityEngine.InputSystem;

// public class HandController : MonoBehaviour
// {
//     public InputActionReference gripInput;
//     public InputActionReference triggerInput;
    
//     private Animator animator;
//     private bool wasPressed = false;

//     private void Start()
//     {
//         animator = GetComponent<Animator>();

//         gripInput.action.Enable();
//         triggerInput.action.Enable();
//     }

//     void Update()
//     {
//         if (animator == null)
//         {
//             return;
//         }

//         bool isPressed = gripInput.action.IsPressed();

//         if (isPressed && !wasPressed)
//         {
//             Debug.Log("Nappia painetaan!");
//         }
        
//         wasPressed = isPressed;

//         float grip = gripInput.action.ReadValue<float>();
//         float trigger = triggerInput.action.ReadValue<float>();


//         //Debug.Log("gribb" + grip);
//         //Debug.Log("trigger" + trigger);
//         animator.SetFloat("Grip", grip);
//         animator.SetFloat("Trigger", trigger);
//     }
// }