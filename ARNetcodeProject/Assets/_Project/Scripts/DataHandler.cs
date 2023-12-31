using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Netcode;

namespace sks {
    public class DataHandler : MonoBehaviour {

        [Header("Data")]
        [SerializeField] string dataKey;
        [TextArea]
        public string metaData;
        public PlayerManager playerManager;

        //Events
        public static event Action<string> OnMetaDataUpdate;
        private void Start() {
            playerManager = Utils.FindComponentInSelfOrParents<PlayerManager>(transform);
        }

        public void SetDatatMessageFromCSVFile(string dataKey) {
            this.dataKey = dataKey;
            //StartCoroutine(GetRequest("C:\\Users\\suman\\Desktop\\csvfile\\csvfile.csv"));
            StartCoroutine(GetRequest(
                "https://nnedigitaldesignstorage.blob.core.windows.net/candidatetasks/Metadata.csv?sp=r&st=2021-03-15T09:12:39Z&se=2024-11-05T17:12:39Z&spr=https&sv=2020-02-10&sr=b&sig=oyj3Qyg4W42%2BO0d7YqmjxmKk0k%2BLVmE243ixdLaq3gk%3D"
                ));
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
                        //var text = webRequest.downloadHandler.text.TrimStart().TrimEnd();
                        var text = webRequest.downloadHandler.text.Trim();


                        string[] records = text.Split('\n');
                        Dictionary<int, string[]> recordCcolumnsKVP = new Dictionary<int, string[]>(records.Length);
                        string newMetaData = "";
                        for (int i = 0; i < records.Length; i++) {
                            string[] recordValues = records[i].Split(new char[] { '\u0009', ',', ';' });
                            if (recordValues.Length > 1 && recordValues[1] == dataKey) { // if the data key is found in the csv file then get this array as data message.
                                /*foreach (string recordValue in recordValues) {
                                    newMetaData += recordValue + ", ";
                                }
                                newMetaData = newMetaData.Remove(newMetaData.Trim().Length - 1);//remove the last comma.*/
                                for (int j = 0; j < recordValues.Length; j++) {
                                    if (j < recordValues.Length - 1) {
                                        newMetaData += recordValues[j] + ", ";
                                    } else {
                                        newMetaData += recordValues[j];
                                    }
                                }
                            }
                            recordCcolumnsKVP.Add(i, recordValues); //Note /u0009 is tab other way to do is \t
                        }
                        metaData = newMetaData;
                        /*if (playerManager.playerSynchroniser.dataShowClientID== (int)NetworkManager.Singleton.LocalClientId) {
                            OnMetaDataUpdate?.Invoke(metaData);
                        }*/

                        OnMetaDataUpdate?.Invoke(metaData);

                        break;
                }
            }
        }

        
    }
}