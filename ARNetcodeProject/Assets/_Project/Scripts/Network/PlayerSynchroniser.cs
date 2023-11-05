using Unity.Netcode;
using UnityEngine;
namespace sks {
    public class PlayerSynchroniser : NetworkBehaviour {

        public NetworkVariable<PlayerOrientation> playerOrientation = new NetworkVariable<PlayerOrientation>(default, NetworkVariableReadPermission.Owner, NetworkVariableWritePermission.Owner);
        [SerializeField]PlayerManager playerManager;
        public Vector3 newVal;
        private void Start() {
            playerManager = GetComponent<PlayerManager>();

        }

        public override void OnNetworkSpawn() {
            playerOrientation.OnValueChanged += OnStateChanged;
        }

        public override void OnNetworkDespawn() {
            playerOrientation.OnValueChanged -= OnStateChanged;
        }

        public void OnStateChanged(PlayerOrientation previous, PlayerOrientation current) {
            Debug.Log("1");
            //playerManager.player.transform.position = current.position;
            //playerManager.player.transform.rotation = current.rotation;
            Debug.Log("1");

            newVal = current.position;
        }
    }
}