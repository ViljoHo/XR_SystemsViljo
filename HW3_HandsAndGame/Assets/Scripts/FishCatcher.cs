using UnityEngine;

public class FishCatcher : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject hookedFish = null;

    private void OnCollisionEnter(Collision collision)
    {
        Transform t = collision.transform;
        if (t && t.tag.ToLower()=="hole")
        {   

            Debug.Log("Siima osui reikään! Kala tarttui kiinni!");
            hookedFish = Instantiate(fishPrefab, transform.position, Quaternion.identity);
            hookedFish.transform.SetParent(this.transform); // Kiinnitetään siiman päähän
        }

    }

}

