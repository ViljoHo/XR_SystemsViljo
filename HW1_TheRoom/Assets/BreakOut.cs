using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionReference  action;
    int state = 0;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (state == 0)
            {
                transform.position = new Vector3(30, 7, 0);
                state = 1;
            } else if (state == 1)
            {
                transform.position = new Vector3(0, 0, 0);
                state = 0;
            }

        };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
