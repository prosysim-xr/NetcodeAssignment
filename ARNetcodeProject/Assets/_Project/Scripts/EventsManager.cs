using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sks {
    public class EventsManager : MonoBehaviour {

        public static EventsManager instance;

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public delegate void OnPlayerJoined(PlayerManager playerManager);
        public event OnPlayerJoined onPlayerJoined;
        public void PlayerJoined(PlayerManager playerManager) {
            if (onPlayerJoined != null) {
                onPlayerJoined(playerManager);
            }
        }

        public delegate void OnPlayerLeft(PlayerManager playerManager);
        public event OnPlayerLeft onPlayerLeft;
        public void PlayerLeft(PlayerManager playerManager) {
            if (onPlayerLeft != null) {
                onPlayerLeft(playerManager);
            }
        }

        public delegate void OnPlayerDied(PlayerManager playerManager);
        public event OnPlayerDied onPlayerDied;
        public void PlayerDied(PlayerManager playerManager) {
            if (onPlayerDied != null) {
                onPlayerDied(playerManager);
            }
        }

        public delegate void OnPlayerRespawned(PlayerManager playerManager);
        public event OnPlayerRespawned onPlayerRespawned;
        public void PlayerRespawned(PlayerManager playerManager) {
            if (onPlayerRespawned != null) {
                onPlayerRespawned(playerManager);
            }
        }

        public delegate void OnPlayerScored(PlayerManager playerManager);
        public event OnPlayerScored onPlayerScored;
        public void PlayerScored(PlayerManager playerManager) {
            if (onPlayerScored != null) {
                onPlayerScored(playerManager);
            }
        }

        public delegate void OnPlayerKilled(PlayerManager playerManager);
        public event OnPlayerKilled onPlayerKilled;
        public void PlayerKilled(PlayerManager playerManager) {
            if (onPlayerKilled != null) {
                onPlayerKilled(playerManager);
            }
        }


        // Relevent events
        public event Action OnGameHostedOrJoined;
        public void InvokeGameHostedOrJoined() {
            OnGameHostedOrJoined?.Invoke();
        }
    }
}