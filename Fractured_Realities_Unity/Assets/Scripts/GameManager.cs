using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    public Vector3 lastPlayerPosition; // Laatste bekende positie van de speler
    public string lastDoorName;       // Laatste gebruikte deur

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Blijf bestaan tussen scènes
        }
        else
        {
            Destroy(gameObject); // Voorkom dubbele GameManagers
        }
    }
}
