using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] RectTransform crosshair;


    private void Start() {
        
    }
    private void Update() {
        // Ray Cast from crosshair position into z direction of camera
        Ray ray = Camera.main.ScreenPointToRay(crosshair.position);
        RaycastHit hit;
        // If ray hits something with box collider and the name of it is "Panel" then Debug.Log("Hit Panel")
        if (Input.GetMouseButtonDown(0)) { 
            if (Physics.Raycast(ray, out hit, 100f) && hit.collider.name == "Panel") {
                Color colorOfPanelQR = hit.collider.GetComponent<MeshRenderer>().material.color;
                Debug.Log($"Hit Panel and Color is {colorOfPanelQR}");

            }
        }

        //Dictionary holding colorOfPanel as key and a Tuple of  position of Panel, location of AllignmentModel and orientation of AllignmentModel



    }


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
}
