using Unity.VisualScripting; 
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ParkourTeleport : MonoBehaviour
{
    public Transform tpNaar;
    //public string requiredBlockTag = "TPBLOCK"; wrm? vloer heeft string tpblock?
    public GameObject player;


    private void OnTriggerEnter(Collider other) //character controller spreekt teleportere tegen(effe uitzetten)
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("AANGERAATKT");

        // Controleer of de speler in aanraking komt met een object met de tag "TPBLOCK"
        if (other.CompareTag("Player") )// gameObject.CompareTag(requiredBlockTag)) wrm?
        {
            CharacterController CC = player.GetComponent<CharacterController>();
            CC.enabled = false;
            player.transform.position = tpNaar.transform.position;
            player.transform.rotation = tpNaar.transform.rotation;
            CC.enabled = true;
            Debug.Log("TELEPORT SUCCESVOL");
        }
        else
        {
            Debug.Log("ERROR NIET TERUH");
        }
    }
}
