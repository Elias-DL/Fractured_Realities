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

    void Start()
    {
        objRandomNr = Random.Range(1, 101);
        feedbackText.text = "Guess a number between 1 and 100!";
        sleutel.SetActive(false);
    }

    public void CheckGuess()
    {
        int playerGuess;
        if (int.TryParse(guessInput.text, out playerGuess))
        {
            if (playerGuess < objRandomNr)
            {
                feedbackText.text = "Too LOW LOSER! Try again.";
            }
            else if (playerGuess > objRandomNr)
            {
                feedbackText.text = "Too HIGH LOSER! Try again.";
            }
            else
            {
                feedbackText.text = "Correct! You guessed it!";
                sleutel.SetActive(true);
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
