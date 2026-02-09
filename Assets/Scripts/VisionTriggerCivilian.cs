using UnityEngine;

public class VisionTriggerCivilian : MonoBehaviour
{
    private CivilianMovement civilian;

    void Awake()
    {
        civilian = GetComponentInParent<CivilianMovement>();

        if (civilian == null)
            Debug.LogError("CivilianMovement EI löytynyt parentista!", this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("👁 Civilian näki pelaajan");

        civilian.SetThreat(other.transform);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("👋 Pelaaja poistui civilianin näkökentästä");

        civilian.ClearThreat();
    }
}
