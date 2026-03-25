using UnityEngine;

public class VisionTriggerVillain : MonoBehaviour
{
    // Inspectorissa asetettavat layermaskit
    public LayerMask obstacleMask; // seinät / esteet
    public LayerMask playerMask;   // pelaaja

    // Viittaukset
    private VillainMovement villain;   // itse vihollisen liikelogiikka
    private Transform playerInRange;    // pelaaja vision triggerin sisällä
    private Rigidbody2D villainRb;      // vihollisen Rigidbody2D

    // Tila: näkeekö vihollinen pelaajan tällä hetkellä oikeasti
    private bool hasLineOfSight;

    // ===== HERÄÄMINEN =====
    // Haetaan tarvittavat komponentit kerran
    void Awake()
    {
        villain = GetComponentInParent<VillainMovement>();
        villainRb = villain.GetComponent<Rigidbody2D>();
    }

    // ===== TRIGGERIIN SISÄÄNTULO =====
    // Pelaaja on näkökentässä, mutta ei vielä välttämättä näkyvissä (seinä voi olla välissä)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = other.transform;
        CheckLineOfSight();
    }

    // ===== TRIGGERISTÄ POISTUMINEN =====
    // Pelaaja poistuu näkökentästä kokonaan
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = null;
        hasLineOfSight = false;
        villain.ClearTarget();

        //Debug.Log("👁 Villain kadotti pelaajan (triggeristä ulos)");
    }

    // ===== JATKUVA NÄKÖTARKISTUS =====
    // Tarkistetaan näköyhteys niin kauan kuin pelaaja on triggerin sisällä
    void FixedUpdate()
    {
        if (playerInRange != null)
            CheckLineOfSight();
    }

    // ===== NÄKÖYHTEYDEN TARKISTUS =====
    // Raycast selvittää onko seinä vihollisen ja pelaajan välissä
    void CheckLineOfSight()
    {
        Vector2 origin = villainRb.position;
        Vector2 target = playerInRange.position;
        Vector2 dir = target - origin;
        float dist = dir.magnitude;

        Debug.DrawRay(origin, dir.normalized * dist, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            dir.normalized,
            dist,
            obstacleMask | playerMask
        );

        if (hit.collider == null)
            return;

        bool seesPlayerNow = hit.collider.CompareTag("Player");

        if (seesPlayerNow && !hasLineOfSight)
        {
            hasLineOfSight = true;
            villain.SetTarget(playerInRange);
            //Debug.Log("👁 Villain näkee pelaajan");
        }
        else if (!seesPlayerNow && hasLineOfSight)
        {
            hasLineOfSight = false;
            villain.ClearTarget();
            //Debug.Log("👁 Villain menetti näköyhteyden");
        }
    }
}