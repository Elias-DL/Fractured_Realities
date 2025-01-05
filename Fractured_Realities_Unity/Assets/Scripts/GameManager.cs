using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 lastPlayerPosition;
    public string lastDoorName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Blijf bestaan bij het wisselen van scenes
        }
        else
        {
            Destroy(gameObject); // Voorkom duplicaten
        }
    }
}
