using UnityEngine;
using UnityEngine.TextCore.Text;

public class ParkourTeleport : MonoBehaviour
{
    public GameObject tpNaar;
    public GameObject player;
    public GameObject Managers;
    public void Start()
    {
    }
    private void OnTriggerEnter(Collider other) //character controller spreekt teleportere tegen(even uitzetten)
    {
        player = GameObject.FindWithTag("Player");
        Managers = GameObject.FindWithTag("Managers");
        Debug.Log("AANGERAATKT");

        // Controleer of de speler in aanraking komt met een object met de tag "TPBLOCK"
        if (other.CompareTag("Player"))
        {
            
            Managers.GetComponent<PlayerStats>().Respawn(tpNaar); // als dit gebeurt wordt de speler terug naar het begin geteleporteerd

            //CC.enabled = true;
            Debug.Log("TELEPORT SUCCESVOL");    



        }
        else
        {
            Debug.Log("ERROR NIET TERUH");
        }
    }
}
