using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera m_MainCamera;
    public Camera m_ZoomCamera;

    public Transform lensObject;


    private Vector3 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


        Vector3 direction = m_ZoomCamera.transform.position - m_MainCamera.transform.position;

        m_ZoomCamera.transform.rotation = Quaternion.LookRotation(direction, transform.up);


        
    }
}
