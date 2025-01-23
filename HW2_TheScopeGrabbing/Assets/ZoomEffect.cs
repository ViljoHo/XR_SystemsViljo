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
        

        //Debug.Log(m_MainCamera.transform.position);

        Vector3 direction = m_ZoomCamera.transform.position - m_MainCamera.transform.position;

        m_ZoomCamera.transform.rotation = Quaternion.LookRotation(direction, transform.up);

        //jos -90 < transform.position.x < 90 tai -90 < transform.position.y < 90

        // EITOIMI
        // if (-90 < transform.rotation.x && transform.rotation.x < 90 &&
        //     -90 < transform.rotation.y && transform.rotation.y < 90)
        // {
        //     lensObject.rotation = Quaternion.Euler(0, 0, 0);
        // } else 
        // {
        //     lensObject.rotation = Quaternion.Euler(0, 180, 0);
        // }
        // EITOIMI

        // if (Vector3.Dot(transform.forward, direction.normalized) > 0)
        // {
        //     m_ZoomCamera.transform.rotation *= Quaternion.Euler(0, 180, 0);
        // }

        
    }
}
