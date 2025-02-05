using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DBConnection : MonoBehaviour
{
    // URL of the PHP script
    private string phpUrl = "http://localhost/UnityDBScripts/Pages/connect.php";

    void Start()
    {
       
    }
    public void OnConnectButtonClick()
    {
        StartCoroutine(SendRequest());
    }

        IEnumerator SendRequest()
    {
        // Send a GET request to the PHP script
        UnityWebRequest www = UnityWebRequest.Get(phpUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            // Log error if the connection fails
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Log the PHP response (like "Connected successfully" or any message from PHP)
            Debug.Log("Response: " + www.downloadHandler.text);
            Debug.Log("VERBIDNING GESLAAGD");
        }
    }


}


