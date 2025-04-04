using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DBConnection : MonoBehaviour
{
    // globaal de php scripts declareren
    private string phpUrlSturen = "http://localhost/Fractured_Realities_DB/UnityDBScripts/Pages/SendData.php";
    private string phpUrlHalen = "http://localhost/Fractured_Realities_DB/UnityDBScripts/Pages/GetData.php";
    public GameObject UserInformationContent;
    public GameObject UserInfoPrefab;

    public GameObject Managers;
    public string username;
    public TMPro.TMP_InputField usernameInput;
    public TMPro.TMP_Text feedbackText;

    void Start()
    {
        Managers = GameObject.FindWithTag("Managers");

        if (PlayerPrefs.HasKey("Username")) // de username blijft binnen de variabelen zodat deze wordt onthouden bij het verzenden van de data
        {
            username = PlayerPrefs.GetString("Username");
            Debug.Log("Loaded Username: " + username);
        }
    }


    private IEnumerator GatherData()
    {
        string usernamedb = username;
      
        int deaths = Managers.GetComponent<PlayerStats>().deaths;
        float time = Managers.GetComponent<PlayerStats>().time;
        Debug.Log("Username : " + username + "deaths : " + deaths + " , time : " + time);
        yield return StartCoroutine(SendRequest(time, deaths, usernamedb)); // Wacht op upload
    }

    public void StartUploadProcess()
    {
        StartCoroutine(GetUploadData());
    }
    private IEnumerator GetUploadData()
    {
        yield return StartCoroutine(GatherData()); // Wacht tot GatherData klaar is zodat de gegevens van het huidige spel ook worden getoond op het scorebord
        yield return StartCoroutine(DataHalenCoroutine()); // Wacht tot DataHalen klaar is
    }
    public void Username()
    {
        if ( usernameInput.text == "")
        {
            feedbackText.text = "Enter a valid username";
        }
        else
        {

            PlayerPrefs.SetString("Username", usernameInput.text);
            PlayerPrefs.Save();
            username = usernameInput.text;
            SceneManager.LoadScene("Main Menu");
            //Debug.Log(username);

        }
    }
    IEnumerator SendRequest( float time, int deaths, string username)
    {

        WWWForm form = new WWWForm();
        form.AddField("username", (string)username);
        form.AddField("time", (int)time);
        form.AddField("deaths", deaths);
        Debug.Log("Sending POST data: Username = " + username+ " Time = " + time + ", Deaths = " + deaths);

        // Get request naar het php script
        UnityWebRequest www = UnityWebRequest.Post(phpUrlSturen, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.downloadHandler.text);
            Debug.Log("VERBIDNING GESLAAGD");
        }
    }

    private IEnumerator DataHalenCoroutine()
    {
        yield return StartCoroutine(GetRequest(phpUrlHalen));
    }

    IEnumerator GetRequest(string uri)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    
                    string rawResponse = webRequest.downloadHandler.text;
                    string[] users = rawResponse.Split('*');
                    for(int i = 0; i < users.Length; i++)
                    {
                        //Debug.Log("current data " + users[i]);
                        if (users[i] != "")
                        {
                            string[] userinfo = users[i].Split(",");    // De gegevens worden apart gesplists door een komma en erna in unity getoond.
                            Debug.Log("Username : " + userinfo[0] + " Deaths : " + userinfo[1] + " Time : " + userinfo[2] + " Date " + userinfo[3]);

                            GameObject gameobj = (GameObject)Instantiate(UserInfoPrefab);
                            gameobj.transform.SetParent(UserInformationContent.transform);
                            gameobj.GetComponent<UserInfo>().Username.text = userinfo[0];
                            gameobj.GetComponent<UserInfo>().Deaths.text = userinfo[1];
                            gameobj.GetComponent<UserInfo>().Time.text = userinfo[2];
                            gameobj.GetComponent<UserInfo>().Date.text = userinfo[3];


                        }


                    }
                    break;
            }
        }
    }
}
