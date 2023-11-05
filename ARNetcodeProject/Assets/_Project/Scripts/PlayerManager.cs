using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using Unity.Netcode;
using StarterAssets;
using UnityEngine.InputSystem;

namespace sks {
    public class PlayerManager : MonoBehaviour {
        [Header("References")]
        public GameObject player;
        public string playerName = "User";
        [SerializeField] GameObject modelToAllign;
        [SerializeField] DataHandler dataHandler;
        [SerializeField] UIController uiController;
        [SerializeField] PlayerInfo playerInfo;
        [SerializeField] RayCastHandler rayCastHandler;
        [Space]
        [SerializeField] Color[] colorArray;
        [SerializeField] Transform[] showCaseRoomQRArray;
        [SerializeField] Transform[] modelTOAllignQRArray;

        // colorToQRs is Tuple collection of  showCaseRoomQR and modelToAllignQR.
        Dictionary<Color, Tuple<Transform, Transform>> colorToQRs = new Dictionary<Color, Tuple<Transform, Transform>>();

        public DataMesage dataMesage;
        public NetworkObject networkObject;
        public PlayerSynchroniser playerSynchroniser;
        private void Start() {
            networkObject = GetComponent<NetworkObject>();
            playerSynchroniser = GetComponent<PlayerSynchroniser>();
            StartCoroutine(nameof(Init));
        }

        private IEnumerator Init() {
            yield return new WaitUntil(() => ServiceLocator.instance != null);
            ServiceLocator.instance.playerManagerDict.Add((int)networkObject.OwnerClientId , this);

            if(networkObject.IsOwner){
                Transform parent = player.transform.parent;//Recheck this
                for (int i = 0; i < parent.childCount; i++) {
                    parent.GetChild(i).gameObject.SetActive(true);
                }
                player.GetComponent<CharacterController>().enabled = true;
                player.GetComponent<FirstPersonController>().enabled = true;
                player.GetComponent<PlayerInput>().enabled = true;
                rayCastHandler.isRayCasterHandlerReady = true;
            }
            

            playerName = "Player " + networkObject.OwnerClientId.ToString();
            gameObject.name = playerName;

            showCaseRoomQRArray = ServiceLocator.instance.showCaseRoomQRArray;
            // Add all the colors and QRs to the dictionary
            for (int i = 0; i < colorArray.Length; i++) {
                colorToQRs.Add(colorArray[i], new Tuple<Transform, Transform>(showCaseRoomQRArray[i], modelTOAllignQRArray[i]));
            }

            DataHandler.OnMetaDataUpdate += OnMetaDataUpdate;

            

            

        }

        private void Update() {


            if (networkObject.IsOwner) {
                playerSynchroniser.OnStateChanged(
                    playerSynchroniser.playerOrientation.Value, 

                    new PlayerOrientation() {
                    position = player.transform.position,
                    rotation = player.transform.rotation
                });
            }

        }

        public void AllignModel(Color showCaseRoomQRColor) {
            colorToQRs.TryGetValue(showCaseRoomQRColor, out var qrInfo);
            Transform showCaseRoomQRTr = qrInfo.Item1;
            Transform modelToAllignQRTr = qrInfo.Item2;
            GameObject go = new GameObject("AllignmentModelTemp");
            go.transform.parent = modelToAllignQRTr;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;

            go.transform.parent = showCaseRoomQRTr.root;
            //go.transform.parent = null;

            modelToAllign.transform.parent = go.transform;

            go.transform.position = showCaseRoomQRTr.position;
            go.transform.rotation = showCaseRoomQRTr.rotation;

            modelToAllign.transform.parent = null;
            Destroy(go);

        }

        public string GetDataKeyForGivenDataTag(Tagger tagger) {
            if (tagger.groupTag != Tagger.GroupTag.Data) {
                Debug.LogError("Tagger is not of type Data");
                return "";
            }
            string dataKey = "";
            switch (tagger.tag) {
                case Tagger.Tag.Alpha:
                    dataKey = "Red";
                    break;
                case Tagger.Tag.Beta:
                    dataKey = "Green";
                    break;
                case Tagger.Tag.Gama:
                    dataKey = "Blue";
                    break;
                case Tagger.Tag.Delta:
                    dataKey = "Yellow";
                    break;
                default:
                    break;
            }
            return dataKey;
        }
        public void SetDataMessage(Tagger tagger) {
            string dataKey = GetDataKeyForGivenDataTag(tagger);
            if(dataHandler !=null) dataHandler.SetDatatMessageFromCSVFile(dataKey);

        }
        private void OnMetaDataUpdate(string metaData) {
            string heading = playerName +" Clicked, Metadata is:";
            dataMesage = new DataMesage(heading, metaData);
            uiController.SetDataMessage(dataMesage);
            uiController.OnOpenMessageBox();
        }
    }

    [Serializable]
    public class DataMesage {
        public string heading;
        public string metaData;
        public DataMesage(string heading, string metaData) {
            this.heading = heading;
            this.metaData = metaData;
        }
    }
}