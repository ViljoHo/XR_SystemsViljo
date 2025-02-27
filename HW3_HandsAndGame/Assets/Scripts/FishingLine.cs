
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FishingLine : MonoBehaviour
{
    public InputActionReference triggerInput;
    public InputActionReference secondaryButtonInput;
    public Transform fingerTip; // Sormen pää, mistä siima lähtee
    public float maxLength = 5f; // Siiman maksimipituus
    public float retractSpeed = 10f; // Kuinka nopeasti siima kelautuu takaisin
    public LineRenderer lineRenderer; // Viiva, joka esittää siimaa
    public GameObject fishingHookPrefab;

    private GameObject fishingHook;

    private Vector3 targetPosition;
    private bool isCasting = false; // Onko siima ulkona?
    private bool isRetracting = false; // Estetään moninkertainen kelautuminen

    void Start()
    {
        fishingHook = Instantiate(fishingHookPrefab, Vector3.zero, Quaternion.identity);
        triggerInput.action.Enable();
        secondaryButtonInput.action.Enable();
        
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false; // Piilotetaan viiva alussa

        targetPosition = fingerTip.position;
    }

    void Update()
    {
        bool triggerIsPressed = triggerInput.action.IsPressed();
        bool secondaryButtonIsPressed = secondaryButtonInput.action.IsPressed();

        if (secondaryButtonIsPressed && triggerIsPressed && !isCasting)
        {
            CastLine();
        }
        else if (!secondaryButtonIsPressed && triggerIsPressed && isCasting && !isRetracting)
        {
            StartCoroutine(RetractCoroutine());
        }

        if (isCasting || isRetracting)
        {
            lineRenderer.SetPosition(0, fingerTip.position);
            lineRenderer.SetPosition(1, targetPosition);
            fishingHook.transform.position = targetPosition;
        }
    }

    void CastLine()
    {
        isCasting = true;
        isRetracting = false;
        lineRenderer.enabled = true; // Näytetään siima
        fishingHook = Instantiate(fishingHookPrefab, Vector3.zero, Quaternion.identity);
        targetPosition = fingerTip.position + Vector3.down * maxLength;
        
    }

    IEnumerator RetractCoroutine()
    {
        isRetracting = true;

        while (Vector3.Distance(targetPosition, fingerTip.position) > 0.1f)
        {
            targetPosition = Vector3.Lerp(targetPosition, fingerTip.position, Time.deltaTime * retractSpeed);
            yield return null;
        }

        targetPosition = fingerTip.position;
        isCasting = false;
        isRetracting = false;
        lineRenderer.enabled = false; // Piilotetaan siima

        if (fishingHook.transform.childCount > 0)
        {
            Transform fish = fishingHook.transform.GetChild(0); // Oletetaan, että kala on ensimmäinen lapsi
            fish.SetParent(null); // Irrota kala, jotta se ei tuhoudu
        }
        Destroy(fishingHook);
    }
}

