using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    // Animation
    public InputActionReference gripInput;
    public InputActionReference triggerInput;

    private Animator animator;
    


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

        float grip = gripInput.action.ReadValue<float>();
        float trigger = triggerInput.action.ReadValue<float>();
        animator.SetFloat("Grip", grip);
        animator.SetFloat("Trigger", trigger);

            
    }

    private void FixedUpdate()
    {
        PhysicsMove();
    }

    private void PhysicsMove()
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


    
}


