using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera m_MainCamera;
    public Camera m_ZoomCamera;


    private Vector3 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //direction = m_MainCamera.transform.eulerAngles + m_ZoomCamera.transform.eulerAngles;
        //direction = m_MainCamera.transform.position - m_ZoomCamera.transform.position;
        // Debug.Log(m_MainCamera.transform.position);
        // Debug.Log(m_MainCamera.transform.rotation);
        // Debug.Log(m_ZoomCamera.transform.position);
        // Debug.Log(m_ZoomCamera.transform.rotation);
        //m_ZoomCamera.transform.eulerAngles = direction;

        // Asetetaan suurennuskamera suoraan pääkameran suuntaan ilman suurennuslasin orientaation vaikutusta
        m_ZoomCamera.transform.rotation = Quaternion.LookRotation(m_MainCamera.transform.forward, Vector3.up);

        //m_ZoomCamera.transform.rotation = m_MainCamera.transform.rotation;

        // Pidä kameran sijainti suurennuslasin mukana
        m_ZoomCamera.transform.position = transform.position;

        // Lasketaan suunta pääkamerasta suurennuslasiin
        //Vector3 directionToLens = transform.position - m_MainCamera.transform.position;

        // Asetetaan suurennuskamera katsomaan tähän suuntaan (estetään kiertyminen)
        //m_ZoomCamera.transform.rotation = Quaternion.LookRotation(directionToLens, Vector3.up);

        // Kameran tulee olla suurennuslasin kohdalla
        // m_ZoomCamera.transform.position = transform.position;



        // 1. Lasketaan suuntavektori pääkamerasta suurennuslasiin
        //Vector3 offset = transform.position - m_MainCamera.transform.position;

        // 2. Kameran näkymän tulee pysyä pääkameran katselusuunnassa
        //m_ZoomCamera.transform.rotation = m_MainCamera.transform.rotation;

        // 3. Kameran tulee olla linssin kohdalla mutta liikkua suhteessa offsettiin
        //Vector3 zoomCameraPosition = m_MainCamera.transform.position + offset;
        //m_ZoomCamera.transform.position = zoomCameraPosition;
        
    }
}
