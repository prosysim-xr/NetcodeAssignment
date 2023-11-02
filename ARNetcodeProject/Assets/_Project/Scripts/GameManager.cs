using System.Collections.Generic;
using System;
using UnityEngine;
namespace sks {
    public class GameManager : Singleton<GameManager> {
        [SerializeField] GameObject player;
        [SerializeField] GameObject modelToAllign;

        [SerializeField] Color[] colorArray;
        [SerializeField] Transform[] showCaseRoomQRArray;
        [SerializeField] Transform[] modelTOAllignQRArray;

        // colorToQRs is showCaseRoomQR and modelToAllignQR.
        private Dictionary<Color, Tuple<Transform, Transform>> colorToQRs = new Dictionary<Color, Tuple<Transform, Transform>>();


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

        /*

            public void PanelData() { // use following to click and correctly orient once this is done Same Data can be sent to Multiplayer and they can update there model aswell.
                // Create a dictionary to hold the data
                Dictionary<Color, Tuple<Vector3, Vector3, Quaternion>> panelDictionary = new Dictionary<Color, Tuple<Vector3, Vector3, Quaternion>>();



                // Replace with the actual color
                //Color (Key)
                Color colorOfPanel = Color.red;

                //PanelData (Value)
                Vector3 qrPanelPos = new Vector3(1, 2, 3); // Replace with the actual position
                Vector3 modelToAllignPos = new Vector3(4, 5, 6); // Replace with the actual location
                Quaternion modelToAllignRot = Quaternion.identity; // Replace with the actual rotation

                // Define the data structure for the tuple
                Tuple<Vector3, Vector3, Quaternion> panelData = new Tuple<Vector3, Vector3, Quaternion>(qrPanelPos, modelToAllignPos, modelToAllignRot);

                panelDictionary.Add(colorOfPanel, panelData);
                // You can add more entries to the dictionary as needed.
                // Repeat the above steps with different values of colorOfPanel and panelData.


                // To access the data for a specific colorOfPanel:
                if (panelDictionary.TryGetValue(Color.red, out var panelInfo)) {
                    Vector3 positionOfPanel = panelInfo.Item1;
                    Vector3 locationOfAlignmentModel = panelInfo.Item2;
                    Quaternion orientationOfAlignmentModel = panelInfo.Item3;

                    // Now you have access to the data for the "red" panel.
                }
            }
        */

    }
}