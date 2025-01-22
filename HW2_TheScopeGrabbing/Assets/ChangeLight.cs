using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeLight : MonoBehaviour
{
    public Light pointLight;
    public InputActionReference  action;
    int state = 0;
    void Start()
    {
        pointLight = GetComponent<Light>();
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (state == 0)
            {
                pointLight.color = Color.red;
                state = 1;
            } else if (state == 1)
            {
                pointLight.color = Color.white;
                state = 0;
            }
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
