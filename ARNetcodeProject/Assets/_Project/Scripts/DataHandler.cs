using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DataHandler : MonoBehaviour {

    [ContextMenu(nameof(ProcessCSVFile))]
    void ProcessCSVFile() {
        //StartCoroutine(GetRequest("C:\\Users\\suman\\Desktop\\csvfile\\csvfile.csv"));
        StartCoroutine(GetRequest("https://nnedigitaldesignstorage.blob.core.windows.net/candidatetasks/Metadata.csv?sp=r&st=2021-03-15T09:12:39Z&se=2024-11-05T17:12:39Z&spr=https&sv=2020-02-10&sr=b&sig=oyj3Qyg4W42%2BO0d7YqmjxmKk0k%2BLVmE243ixdLaq3gk%3D"));
    }

    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result) {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    var text = webRequest.downloadHandler.text;


                    string[] records = text.Split('\n');
                    Dictionary<int, string[]> recordCcolumnsKVP = new Dictionary<int, string[]>(records.Length);
                    for (int i = 0; i < records.Length; i++) {
                        recordCcolumnsKVP.Add(i, records[i].Split(new char[] { '\u0009', ',', ';' })); //Note /u0009 is tab other way to do is \t
                    }

                    for(int i = 0; i<recordCcolumnsKVP.Keys.Count; i++) {
                        string str = "";
                        for(int j = 0; j < recordCcolumnsKVP[i].Length; j++) {
                            str += recordCcolumnsKVP[i][j] + "   ";
                        }
                        Debug.Log(str);
                    }
                    break;
            }
        }
    }
}