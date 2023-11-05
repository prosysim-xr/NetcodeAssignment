using sks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static T FindComponentInSelfOrParents<T>(Transform selfTr, string matchComponentGOName = null, int iterations = 10 ) where T : Component {
        //in componentGO, find the component if not find then go to parent and find the component till transform root is reached or 10 iterations passed.
        Component component = null;
        Debug.Log("FindComponentInSelfOrParents");
        int i = 0;
        while ((selfTr.root != selfTr || selfTr.root == selfTr)  && i < iterations) {
            Debug.Log("Entered WHile : " + selfTr.name);
            Component c = selfTr.GetComponent<T>();
            if (matchComponentGOName == null) {
                Debug.Log("matchComponentGOName is null");
                if (c != null) {
                    Debug.Log("c is not null");
                    component = c;
                    Debug.Log("component is set " + component.GetType());
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
