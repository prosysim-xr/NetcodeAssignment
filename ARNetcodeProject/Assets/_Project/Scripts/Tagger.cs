using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sks {
    public class Tagger : MonoBehaviour {
        public GroupTag groupTag;
        public Tag tag;
        public enum Tag {
            None = 0,
            RedData = 1,
            GreenData = 2,
            BlueData = 3,
            YellowData = 4,
        }

        public enum GroupTag {
            None = 0,
            Data = 1,
            Model = 2,
            Info = 3
        }
    }
}


