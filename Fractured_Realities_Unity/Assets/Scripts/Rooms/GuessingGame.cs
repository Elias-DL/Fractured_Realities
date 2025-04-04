using UnityEngine;
using UnityEngine.UI;
using TMPro; //NOOIT VERGETEN BRO

public class GuessingGame : MonoBehaviour
{
    public TMPro.TMP_InputField guessInput;
    public TMPro.TMP_Text feedbackText;
    private int objRandomNr;
    public GameObject sleutel;
    public GameObject guessingGameCanvas;
    public GameObject canvas;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        guessingGameCanvas = canvas.GetComponent<MenuLogic>().guessingGameUI;


        sleutel = GameObject.FindWithTag("Key4");

        objRandomNr = Random.Range(1, 101);
        feedbackText.text = "Guess a number between 1 and 100!";
        sleutel.GetComponent<MeshRenderer>().enabled = false;
    }

    public void CheckGuess()
    {
        int playerGuess;
        if (int.TryParse(guessInput.text, out playerGuess)) // controlleeer de invoer van de speler en geef commentaar
        {
            if (playerGuess < objRandomNr)
            {
                feedbackText.text = "Too LOW! Try again.";
            }
            else if (playerGuess > objRandomNr)
            {
                feedbackText.text = "Too HIGH! Try again.";
            }
            else
            {
                feedbackText.text = "Correct! You guessed it!";
                sleutel.GetComponent<MeshRenderer>().enabled = true; // als de gok juist is wordt de sleutel zichtbaar
                guessingGameCanvas.SetActive(false);

            }
        }
        else
        {
            feedbackText.text = "Please enter a valid number!";
        }

        guessInput.text = ""; //reset iput
    }
}
