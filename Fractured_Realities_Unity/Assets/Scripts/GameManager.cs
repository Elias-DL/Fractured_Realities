using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton zodat andere scripts hier toegang toe hebben
    [SerializeField] private GameObject door; // De deur die geactiveerd moet worden
    [SerializeField] private GameObject key;
    private PaintingCube[] allCubes; // Alle kubussen in de scene

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        allCubes = FindObjectsOfType<PaintingCube>(); // Zoek alle kubussen in de scène
    }

    public void CheckAllPaintings()
    {
        foreach (PaintingCube cube in allCubes)
        {
            if (!cube.IsCorrect()) // Als er ook maar één kubus fout is, doe niks
            {
                return;
            }
        }

        Debug.Log("All paintings are correct! Opening door...");
        ActivateDoor();
    }

    private void ActivateDoor()
    {
        if (door != null)
        {
            door.SetActive(true); // Zet de deur actief (of speel een animatie, etc.)
            key.SetActive(true);

        }
    }
}
