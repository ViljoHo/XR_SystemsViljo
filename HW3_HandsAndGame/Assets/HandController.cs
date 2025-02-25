using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode] // Suorittaa koodin myös editorissa ilman pelitilaa
public class HandController : MonoBehaviour
{
    public InputActionReference gripInput;
    public InputActionReference triggerInput;

    private Animator animator;
    private bool wasPressed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (Application.isPlaying) // Estetään turhat kutsut editoritilassa
        {
            gripInput.action.Enable();
            triggerInput.action.Enable();
        }
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
        }
        else // EDITORITILASSA: Pakota animaatio päälle
        {
            animator.Update(0);
        }
    }
}




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