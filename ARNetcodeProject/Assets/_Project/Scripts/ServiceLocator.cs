using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sks {
    public class ServiceLocator : Singleton<ServiceLocator> {

        public PlayerManager myPlayerManager;
        private void Start() {
            // probably using isMine when found multiple players  to set the playerManager
        }
    }
}