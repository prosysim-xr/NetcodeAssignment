using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
namespace sks {
    public class PlayerInfo : MonoBehaviour {
        Transform player;
        Vector3 startingPos = new Vector3(0,0,0);
        [SerializeField]TextMeshPro frontText;
        [SerializeField]TextMeshPro backText;

        PlayerManager playerManager;
        private void Start() {
            StartCoroutine(Init());
        }

        IEnumerator Init() {
            yield return new WaitUntil(() => ServiceLocator.instance != null);
            playerManager = ServiceLocator.instance.playerManager;//Alternatively use  Utils.FindComponentInSelfOrParents to get the playerManager
            player = playerManager.transform;
            // Set randomly the position of the playerFPSSetup around startingPos (to avoid players to be on top of each other)
            player.position = startingPos + new Vector3(Random.Range(-1f, 1f), startingPos.y, Random.Range(-1f, 1f));


            frontText.text = playerManager.playerName;
            backText.text = playerManager.playerName;
        }
    }
}

