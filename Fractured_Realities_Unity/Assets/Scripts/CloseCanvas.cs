using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject guessingGameCanvas;

    public void CloseCanvasFunction()
    {
        guessingGameCanvas.SetActive(false);
    }
}
