using UnityEngine;
using UnityEngine.InputSystem;

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
        gripInput.action.Enable();
        triggerInput.action.Enable();
        // Animation
        animator = GetComponent<Animator>();

        

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

            
    }

    private void FixedUpdate()
    {
        PhysicsMove3();
    }

    private void PhysicsMove3()
    {
        if (_body == null || _followTarget == null) return;

        // Position
        var positionWithOffset = _followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.linearVelocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);


        // Rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {

            if (angle > 180.0f) { angle -= 360.0f; }

            _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

        }

    }

    private void PhysicsMove2()
    {
        if (_body == null || _followTarget == null) return;

        // Position
        Vector3 positionWithOffset = _followTarget.position + positionOffset;
        var distance = Vector3.Distance(_followTarget.position, transform.position);
        float maxSpeed = 10f;
        _body.linearVelocity = (positionWithOffset - transform.position).normalized * Mathf.Clamp(followSpeed * distance, 0, maxSpeed);

        // Rotation
        Quaternion rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        Quaternion deltaRotation = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f) angle -= 360f;
        float maxAngularSpeed = 20f;
        _body.angularVelocity = axis * Mathf.Clamp(angle * Mathf.Deg2Rad * rotateSpeed, -maxAngularSpeed, maxAngularSpeed);
    }


    private void PhysicsMove()
    {
        if (_body == null || _followTarget == null) return;

        //Position
        Vector3 positionWithOffset = _followTarget.position + positionOffset;
        var distance = Vector3.Distance(_followTarget.position, transform.position);
        _body.linearVelocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        //Rotation
        Quaternion rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        Quaternion deltaRotation = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
        
        if (angle > 180f) angle -= 360f;
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

        


        // Position
        //Vector3 positionWithOffset = _followTarget.position + positionOffset;
        //_body.linearVelocity = (positionWithOffset - _body.position) * followSpeed;

        // Rotation
        //Quaternion rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        //Quaternion deltaRotation = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        //deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        //if (angle > 180f) angle -= 360f;
        //_body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

    }
}






//Toimiva versio



// using UnityEngine;
// using UnityEngine.InputSystem;

// [ExecuteInEditMode] // Suorittaa koodin myös editorissa ilman pelitilaa
// public class HandController : MonoBehaviour
// {
//     // Animation
//     public InputActionReference gripInput;
//     public InputActionReference triggerInput;

//     private Animator animator;
//     private bool wasPressed = false;


//     // Physics Movement
//     [SerializeField] private GameObject followObject;
//     [SerializeField] private float followSpeed = 30f;
//     [SerializeField] private float rotateSpeed = 100f;
//     [SerializeField] private Vector3 positionOffset;
//     [SerializeField] private Vector3 rotationOffset;

//     private Transform _followTarget;

//     private Rigidbody _body;




//     private void Start()
//     {
//         // Animation
//         animator = GetComponent<Animator>();

//         if (Application.isPlaying) // Estetään turhat kutsut editoritilassa
//         {
//             gripInput.action.Enable();
//             triggerInput.action.Enable();
//         }

//         // Physic Movement
//         _followTarget = followObject.transform;
//         _body = GetComponent<Rigidbody>();
//         //Debug.Log("_body" + _body);
//         _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
//         _body.interpolation = RigidbodyInterpolation.Interpolate;
//         _body.mass = 20f;

//         // Teleport hands
//         _body.position = _followTarget.position;
//         _body.rotation = _followTarget.rotation;

//     }

//     void Update()
//     {
//         if (animator == null)
//         {
//             return;
//         }

//         if (Application.isPlaying) // PELITILASSA: päivitä inputin mukaan
//         {
//             bool isPressed = gripInput.action.IsPressed();

//             if (isPressed && !wasPressed)
//             {
//                 Debug.Log("Nappia painetaan!");
//             }

//             wasPressed = isPressed;

//             float grip = gripInput.action.ReadValue<float>();
//             float trigger = triggerInput.action.ReadValue<float>();

//             animator.SetFloat("Grip", grip);
//             animator.SetFloat("Trigger", trigger);

//             //PhysicsMove();
//         }
//         else // EDITORITILASSA: Pakota animaatio päälle
//         {
//             animator.Update(0);
//         }
//     }

//     private void FixedUpdate()
//     {
//         PhysicsMove();
//     }


//     private void PhysicsMove()
//     {
//         if (_body == null || _followTarget == null) return;

//         // Position
//         Vector3 positionWithOffset = _followTarget.position + positionOffset;
//         //Vector3 targetPosition = _followTarget.position;
//         _body.linearVelocity = (positionWithOffset - _body.position) * followSpeed;

//         // Rotation
//         Quaternion rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
//         //Quaternion targetRotation = _followTarget.rotation;
//         Quaternion deltaRotation = rotationWithOffset * Quaternion.Inverse(_body.rotation);
//         deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

//         if (angle > 180f) angle -= 360f;
//         _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

//     }
// }
