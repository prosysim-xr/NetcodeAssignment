using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sks {
    public class ServiceLocator : MonoBehaviour {
        public static ServiceLocator instance;

        public PlayerManager playerManager;

        public Transform[] showCaseRoomQRArray;

        private void Awake() {
            if (instance == null) {
                instance = this;//its ok if this is destroyed on scene change
            } else {
                Destroy(gameObject);
            }
        }
        private void Start() {
            // probably using isMine when found multiple players  to set the playerManager
        }
    }
}