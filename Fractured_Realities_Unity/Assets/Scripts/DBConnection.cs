using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DBConnection : MonoBehaviour
{
    // URL of the PHP script
    private string phpUrlSturen = "http://localhost/School/UnityDBScripts/Pages/SendData.php";
    private string phpUrlHalen = "http://localhost/school/UnityDBScripts/Pages/GetData.php";
    public GameObject UserInformationContent;
    public GameObject UserInfoPrefab;

    public GameObject Managers;
    
    void Start()
    {
        Managers = GameObject.FindWithTag("Managers");
        
        int deaths = Managers.GetComponent<PlayerStats>().deaths;
        float time = Managers.GetComponent<PlayerStats>().time;
        Debug.Log("deaths : " + deaths + " , time : " + time);
        StartCoroutine(SendRequest(time, deaths));

    }



    IEnumerator SendRequest( float time, int deaths)
    {

        WWWForm form = new WWWForm();
       
        form.AddField("time", (int)time);
        form.AddField("deaths", deaths);
        Debug.Log("Sending POST data: Time = " + time + ", Deaths = " + deaths);

        // Send a GET request to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(phpUrlSturen, form);
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

    public void DataHalen()
    {
        StartCoroutine(GetRequest("http://localhost/school/UnityDBScripts/Pages/GetData.php"));
    }

    IEnumerator GetRequest(string uri)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
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
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string rawResponse = webRequest.downloadHandler.text;
                    string[] users = rawResponse.Split('*');
                    for(int i = 0; i < users.Length; i++)
                    {
                        //Debug.Log("current data " + users[i]);
                        if (users[i] != "")
                        {
                            string[] userinfo = users[i].Split(",");
                            Debug.Log("ID : " + userinfo[0] + " Deaths : " + userinfo[1] + " Time : " + userinfo[2]);

                            GameObject gameobj = (GameObject)Instantiate(UserInfoPrefab);
                            gameobj.transform.SetParent(UserInformationContent.transform);
                            gameobj.GetComponent<UserInfo>().ID.text = userinfo[0];
                            gameobj.GetComponent<UserInfo>().Deaths.text = userinfo[1];
                            gameobj.GetComponent<UserInfo>().Time.text = userinfo[2];


                        }


                    }
                    break;
            }
        }
    }
}
