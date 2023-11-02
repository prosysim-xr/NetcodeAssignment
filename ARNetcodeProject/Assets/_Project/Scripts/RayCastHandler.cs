using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace sks {
    public class RayCastHandler : MonoBehaviour {
        GameManager gm;
        [SerializeField] RectTransform crosshair;

        // Start is called before the first frame update
        void Start() {
            gm = GameManager.Instance;

        }

        // Update is called once per frame
        void Update() {
            // Ray Cast from crosshair position into z direction of camera
            Ray ray = Camera.main.ScreenPointToRay(crosshair.position);
            RaycastHit hit;
            // If ray hits something with box collider and the name of it is "Panel" then Debug.Log("Hit Panel")

            if (Input.GetMouseButtonDown(0)) {
                if (Physics.Raycast(ray, out hit, 100f) && hit.collider.name == "Panel") {
                    Color showCaseRoomQRColor = hit.collider.GetComponent<MeshRenderer>().material.color;
                    Debug.Log($"Hit Panel and Color is {showCaseRoomQRColor}");

                    gm.AllignModel(showCaseRoomQRColor);
                } else if (Physics.Raycast(ray, out hit, 100f) && hit.collider.GetComponent<Tagger>()?.groupTag == Tagger.GroupTag.Data) {

                    string key = gm.GetKeyForDataTag(hit.collider.GetComponent<Tagger>());
                   




                }
            }

            //Dictionary holding colorOfPanel as key and a Tuple of  position of Panel, location of AllignmentModel and orientation of AllignmentModel


        }
        


    }
}