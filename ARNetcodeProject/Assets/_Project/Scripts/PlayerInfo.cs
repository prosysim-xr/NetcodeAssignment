using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.Netcode;
using UnityEngine;
namespace sks {
    public class PlayerInfo : MonoBehaviour {
        Transform spawnedUser;
        Vector3 startingPos = new Vector3(0,0,0);
        [SerializeField]TextMeshPro frontText;
        [SerializeField]TextMeshPro backText;

        PlayerManager playerManager;
        private void Start() {
            StartCoroutine(Init());
        }

        IEnumerator Init() {
            yield return new WaitUntil(() => ServiceLocator.instance != null);//TODO remove this later as now it is redundant
            playerManager = Utils.FindComponentInSelfOrParents<PlayerManager>(transform);

            if (!playerManager.networkObject.IsOwner) {
                spawnedUser = playerManager.transform;
                // Set randomly the position of the playerFPSSetup around startingPos (to avoid players to be on top of each other)
                spawnedUser.position = startingPos + new Vector3(Random.Range(-1f, 1f), startingPos.y, Random.Range(-1f, 1f));
            }
            frontText.text = playerManager.playerName;
            backText.text = playerManager.playerName;
        }
    }
}

