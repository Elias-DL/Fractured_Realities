using UnityEngine;

public class ParkourTeleport : MonoBehaviour
{
    public Transform tpNaar;
    public string requiredBlockTag = "TPBLOCK";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AANGERAATKT");

        // Controleer of de speler in aanraking komt met een object met de tag "TPBLOCK"
        if (other.CompareTag("Player") && gameObject.CompareTag(requiredBlockTag))
        {
            other.transform.position = tpNaar.position;
            Debug.Log("TELEPORT SUCCESVOL");
        }
        else
        {
            Debug.Log("ERROR NIET TERUH");
        }
    }
}
