using UnityEngine;
using UnityEngine.TextCore.Text;

public class ParkourTeleport : MonoBehaviour
{
    public GameObject tpNaar;
    //public string requiredBlockTag = "TPBLOCK"; wrm? vloer heeft string tpblock?
    public GameObject player;
    public GameObject Managers;
    public void Start()
    {
    }
    private void OnTriggerEnter(Collider other) //character controller spreekt teleportere tegen(effe uitzetten)
    {
        player = GameObject.FindWithTag("Player");
        Managers = GameObject.FindWithTag("Managers");
        Debug.Log("AANGERAATKT");

        // Controleer of de speler in aanraking komt met een object met de tag "TPBLOCK"
        if (other.CompareTag("Player"))// gameObject.CompareTag(requiredBlockTag)) wrm?
        {
            //CharacterController CC = player.GetComponent<CharacterController>();
            //CC.enabled = false;
            //player.transform.position = tpNaar.transform.position;
            //player.transform.rotation = tpNaar.transform.rotation;
            Managers.GetComponent<PlayerStats>().Respawn(tpNaar);

            //CC.enabled = true;
            Debug.Log("TELEPORT SUCCESVOL");    



        }
        else
        {
            Debug.Log("ERROR NIET TERUH");
        }
    }
}
