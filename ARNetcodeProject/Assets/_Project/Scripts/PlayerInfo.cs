using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace sks {
    public class PlayerInfo : MonoBehaviour {
        [SerializeField] Transform playerFPSSetup;
        Vector3 startingPos = new Vector3(0,0,0);
        [SerializeField]TextMeshPro frontText;
        [SerializeField]TextMeshPro backText;

        PlayerManager playerManager;
        private void Start() {
            playerManager = Utils.FindComponentInSelfOrParents<PlayerManager>(transform);
            //playerManager = GetComponentInParent<PlayerManager>();

            // Set randomly the position of the playerFPSSetup around startingPos (to avoid players to be on top of each other)
            playerFPSSetup.position = startingPos + new Vector3(Random.Range(-1f, 1f), startingPos.y, Random.Range(-1f, 1f));


            frontText.text = playerManager.playerName;
            backText.text = playerManager.playerName;
        }
    }
}

