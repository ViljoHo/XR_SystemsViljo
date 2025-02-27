using UnityEngine;

public class Projectile : MonoBehaviour
{   
    public GameObject holePrefab; 

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

            ContactPoint contact = collision.contacts[0];

            if (holePrefab != null)
            {   
                GameObject hole = Instantiate(holePrefab, contact.point + contact.normal * 0.05f, Quaternion.LookRotation(-contact.normal));
                hole.transform.SetParent(collision.transform); 
            }

            Destroy(gameObject);
        }

    }
}




