using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sks {
    public class Tagger : MonoBehaviour {
        public GroupTag groupTag = GroupTag.None;
        public Tag tag = Tag.None;
        public enum Tag {
            None = 0,
            Alpha = 1,
            Beta = 2,
            Gama = 3,
            Delta = 4,
            Epsilon = 5,
        }

        public enum GroupTag {
            None = 0,
            Data = 1,
            Model = 2,
            Info = 3
        }
    }
}


