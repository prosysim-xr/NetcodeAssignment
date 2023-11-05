using sks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static T FindComponentInSelfOrParents<T>(Transform selfTr, string matchComponentGOName = null, int iterations = 10 ) where T : Component {
        //in componentGO, find the component if not find then go to parent and find the component till transform root is reached or 10 iterations passed.
        Component component = null;
        int i = 0;
        while ((selfTr.root != selfTr || selfTr.root == selfTr)  && i < iterations) {
            Component c = selfTr.GetComponent<T>();
            if (matchComponentGOName == null) {
                if (c != null) {
                    component = c;
                    break;
                }
            } else {
                if (c != null && selfTr.parent.name == matchComponentGOName) {
                    component = c;
                    break;
                }
            }
            selfTr = selfTr.parent;
            i++;
        }
        return component as T;
    }
}
