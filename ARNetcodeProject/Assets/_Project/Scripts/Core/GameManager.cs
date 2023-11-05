using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace sks {
    public class GameManager : MonoBehaviour {
        //Singleton
        public static GameManager instance;
        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }
        private void Start() {
        }
        public void JoinGame() {
            NetworkManager.Singleton.StartClient();
            EventsManager.instance.InvokeGameHostedOrJoined();
        }
        public void HostGame() {
            NetworkManager.Singleton.StartHost();
            EventsManager.instance.InvokeGameHostedOrJoined();
        }
    }
}