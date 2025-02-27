using UnityEngine;

public class FishCatcher : MonoBehaviour
{
    public GameObject fishPrefab;
    private GameObject hookedFish = null;

    private void OnCollisionEnter(Collision collision)
    {
        Transform t = collision.transform;
        if (t && t.tag.ToLower()=="hole")
        {   
            hookedFish = Instantiate(fishPrefab, transform.position, Quaternion.identity);
            hookedFish.transform.SetParent(this.transform);
        }

    }

}

