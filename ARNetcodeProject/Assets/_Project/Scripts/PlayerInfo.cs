using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace sks {
    public class PlayerInfo : MonoBehaviour {
        [SerializeField]TextMeshPro frontText;
        [SerializeField]TextMeshPro backText;

        GameManager gm;
        private void Start() {
            gm = GameManager.Instance;
            frontText.text = gm.playerName;
            backText.text = gm.playerName;
        }
    }
}