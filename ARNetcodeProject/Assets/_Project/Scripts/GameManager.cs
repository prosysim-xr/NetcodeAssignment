using System.Collections.Generic;
using System;
using UnityEngine;
namespace sks {
    public class GameManager : Singleton<GameManager> {
        [Header("References")]
        [SerializeField] GameObject player;
        [SerializeField] string playerName = "User";
        [SerializeField] GameObject modelToAllign;
        [SerializeField] DataHandler dataHandler;
        [SerializeField] UIController uiController;

        [Space]
        [SerializeField] Color[] colorArray;
        [SerializeField] Transform[] showCaseRoomQRArray;
        [SerializeField] Transform[] modelTOAllignQRArray;

        // colorToQRs is Tuple collection of  showCaseRoomQR and modelToAllignQR.
        Dictionary<Color, Tuple<Transform, Transform>> colorToQRs = new Dictionary<Color, Tuple<Transform, Transform>>();


        private void Start() {
            // Add all the colors and QRs to the dictionary
            for (int i = 0; i < colorArray.Length; i++) {
                colorToQRs.Add(colorArray[i], new Tuple<Transform, Transform>(showCaseRoomQRArray[i], modelTOAllignQRArray[i]));
            }
        }
        private void Update() {




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
            Debug.Log("/// 1");
            string dataKey = GetDataKeyForGivenDataTag(tagger);
            Debug.Log("/// 2");

            Debug.Log("DataKey: " + dataKey);
            if(dataHandler !=null) dataHandler.SetDatatMessageFromCSVFile(dataKey);
            Debug.Log("/// 13");

        }
    }
}