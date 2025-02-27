using UnityEngine;

public class Projectile : MonoBehaviour
{   
    public GameObject holePrefab; // Reiän prefab

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform t = collision.transform;
        if (t && t.tag.ToLower()=="ice")
        {   


            Debug.Log("pallo osuu jäähän");
            ContactPoint contact = collision.contacts[0];

            if (holePrefab != null)
            {   
                Debug.Log("reikä prefab oikein asetettu");
                GameObject hole = Instantiate(holePrefab, contact.point + contact.normal * 0.01f, Quaternion.LookRotation(-contact.normal));
                hole.transform.SetParent(collision.transform); // Kiinnitetään kohdeobjektiin
            }

            Destroy(gameObject);
        }

    }
}




