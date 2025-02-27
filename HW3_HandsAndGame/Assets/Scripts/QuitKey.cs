using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitKey : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionReference  action;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else   
                Application.Quit();
            #endif
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
