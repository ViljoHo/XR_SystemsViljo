using UnityEngine;
using UnityEngine.InputSystem;

public class HandShooter : MonoBehaviour
{
    public GameObject projectilePrefab; 

    public Transform firePoint; 
    private float shootForce = 5f;

    public InputActionReference gripInput;
    public InputActionReference primaryButton;

    private bool wasPressed = false;

    void Start()
    {
        gripInput.action.Enable();
        primaryButton.action.Enable();
    }

    void Update()
    {
        bool primaryButtonIsPressed = primaryButton.action.IsPressed();
        if (gripInput.action.IsPressed() && primaryButtonIsPressed && !wasPressed)
        {
            Shoot();
        }

        wasPressed = primaryButtonIsPressed;
    }

    void Shoot()
    {


        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        //Vector3 shootDirection = Quaternion.Euler(shootDirectionx, shootDirectiony, shootDirectionz) * firePoint.forward;
        Vector3 shootDirection = Quaternion.Euler(0, 90, -20) * transform.forward;
        rb.linearVelocity = shootDirection * shootForce;
        Destroy(projectile, 5f); 
    }
}
