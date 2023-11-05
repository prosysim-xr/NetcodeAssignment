using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sks {
    public class JoinGameUIController : MonoBehaviour {
        [SerializeField] Button b_host;
        [SerializeField] Button b_client;

        [SerializeField] GameObject joinHostPanel;
        [SerializeField] GameObject startUpCamera;

        // Start is called before the first frame update
        void Start() {
            StartCoroutine(nameof(Init));
        }

        private IEnumerator Init() {
            yield return new WaitUntil(() => ServiceLocator.instance != null);

            b_client.onClick.AddListener(() => { GameManager.instance.JoinGame(); });
            b_host.onClick.AddListener(() => { GameManager.instance.HostGame(); });

            EventsManager.instance.OnGameHostedOrJoined += OnGameHostedOrJoined;
        }

        // Update is called once per frame
        void Update() {

        }
        private void OnDestroy() {
            b_client.onClick.RemoveAllListeners();
            b_host.onClick.RemoveAllListeners();

            EventsManager.instance.OnGameHostedOrJoined -= OnGameHostedOrJoined;
        }



        private void OnGameHostedOrJoined() {
            joinHostPanel.SetActive(false);
            startUpCamera.SetActive(false);
        }
    }
}