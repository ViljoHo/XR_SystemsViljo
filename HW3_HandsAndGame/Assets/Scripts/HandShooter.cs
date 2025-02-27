using UnityEngine;
using UnityEngine.InputSystem;

public class HandShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab ammukselle (pieni pallo)
    public Transform firePoint; // Kohta, josta pallo ammutaan
    public float shootForce = 10f;

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
        Vector3 shootDirection = Quaternion.Euler(0, -90, 0) * firePoint.forward;
        rb.linearVelocity = shootDirection * shootForce;
        Destroy(projectile, 5f); // Poistetaan 5s jälkeen
    }
}
