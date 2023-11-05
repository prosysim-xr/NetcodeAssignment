using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
namespace sks {
    public class ModelToAllign : MonoBehaviour {
        public Transform[] modelTOAllignQRArray;/*
        private void Start() {
            NetworkObject networkObject = GetComponent<NetworkObject>();
            if (networkObject.IsOwner) {
                StartCoroutine(nameof(Init));
            }
        }

        private IEnumerator Init() {
            yield return new WaitUntil(() => ServiceLocator.instance != null);
            ServiceLocator.instance.modelToAllign = gameObject;
        }*/
    }
}