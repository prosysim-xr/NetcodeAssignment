using Unity.Netcode;
using UnityEngine;
namespace sks {
    public class PlayerSynchroniser : NetworkBehaviour {

        //Network Variables
        public NetworkVariable<PlayerOrientation> playerOrientation = new NetworkVariable<PlayerOrientation>(default, NetworkVariableReadPermission.Owner, NetworkVariableWritePermission.Owner);
        [SerializeField]PlayerManager playerManager;
        
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
        }

        //RPCs
        [ServerRpc]
        public void OnQRClickedServerRpc(Color qrColor) {
                Debug.Log("//5");
                ServiceLocator.instance.playerManagerDict[0].AllignModel(qrColor);
                Debug.Log("//6");   
        }
    }


   
}