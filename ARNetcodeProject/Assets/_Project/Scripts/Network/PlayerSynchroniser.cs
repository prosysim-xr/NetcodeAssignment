using Unity.Netcode;
using UnityEngine;
namespace sks {
    public class PlayerSynchroniser : NetworkBehaviour {

        //Network Variables
        public NetworkVariable<PlayerOrientation> playerOrientation = new NetworkVariable<PlayerOrientation>(default, NetworkVariableReadPermission.Owner, NetworkVariableWritePermission.Owner);
        [SerializeField]PlayerManager playerManager;
        public int dataShowClientID = -1;
        //public int count = 0;
        
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
            Debug.Log("//..5");
            if (NetworkManager.Singleton.IsServer) {
                ServiceLocator.instance.playerManagerDict[(int)NetworkManager.ServerClientId].AllignModel(qrColor);
                Debug.Log("//6");
            }
            //OnQRClickedClientRpc(qrColor);
        }

        [ClientRpc]
        public void OnQRClickedClientRpc(Color qrColor) {
            Debug.Log("//..7");
            if (NetworkManager.Singleton.IsServer) {
                playerManager.AllignModel(qrColor);
            }
            Debug.Log("//..8");
        }

        [ServerRpc]
        public void OnDataBoxColorClickedServerRpc(ulong groupTag, ulong tag, ulong clientID) {
            for (int i = 0; i < ServiceLocator.instance.playerManagerDict.Count; i++) {
                OnDataBoxColorClickedClientRpc(groupTag, tag, clientID);
            }
        }

        [ClientRpc]
        public void OnDataBoxColorClickedClientRpc(ulong groupTag, ulong tag, ulong clientID) {
            Tagger tagger = new((int)groupTag, (int)tag);
           /* for(int i = 0; i < ServiceLocator.instance.playerManagerDict.Count; i++) {
                if (ServiceLocator.instance.playerManagerDict[i].networkObject.OwnerClientId == clientID ) {
                    ServiceLocator.instance.playerManagerDict[i].SetDataMessage(tagger);
                }
            }*/
            ServiceLocator.instance.playerManagerDict[(int)clientID].SetDataMessage(tagger);
            dataShowClientID = (int)clientID;
            ServiceLocator.instance.dataShowClientID = (int)clientID;
            //count++;
        }
    }


   
}