using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace sks {
    public class RayCastHandler : MonoBehaviour {
        [Header("References")]
        PlayerManager playerManager;
        [SerializeField] RectTransform crosshair;

        // Start is called before the first frame update
        void Start() {
            playerManager = Utils.FindComponentInSelfOrParents<PlayerManager>(transform);
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
                    playerManager.AllignModel(showCaseRoomQRColor);
                } else if (Physics.Raycast(ray, out hit, 100f) && hit.collider.GetComponent<Tagger>()?.groupTag == Tagger.GroupTag.Data) {
                    Debug.Log("/// ...12");
                    Tagger tagger = hit.collider.GetComponent<Tagger>();
                    Debug.Log("tagger  tag is " + tagger.tag + " for grouptab "+ tagger.groupTag);
                    if(tagger != null) playerManager.SetDataMessage(tagger);

                }
            }

        }
        


    }
}