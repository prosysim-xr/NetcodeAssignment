using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.Netcode;
using UnityEngine;
namespace sks {
    public class PlayerInfo : MonoBehaviour {
        [SerializeField]TextMeshPro frontText;
        [SerializeField]TextMeshPro backText;

        PlayerManager playerManager;
        private void Start() {
            StartCoroutine(Init());
        }

        IEnumerator Init() {
            yield return new WaitUntil(() => ServiceLocator.instance != null);//TODO remove this later as now it is redundant
            playerManager = Utils.FindComponentInSelfOrParents<PlayerManager>(transform);
            frontText.text = playerManager.playerName;
            backText.text = playerManager.playerName;
        }
    }
}

