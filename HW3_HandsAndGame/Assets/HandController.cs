using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public InputActionReference gripInput;
    public InputActionReference triggerInput;
    
    private Animator animator;
    private bool wasPressed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        gripInput.action.Enable();
        triggerInput.action.Enable();
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


        //Debug.Log("gribb" + grip);
        //Debug.Log("trigger" + trigger);
        animator.SetFloat("Grip", grip);
        animator.SetFloat("Trigger", trigger);
    }
}