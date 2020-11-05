using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtentions {

    public static Transform GetChildByName(this Transform parent, string name) {
        Transform innerReturn;
        foreach (Transform child in parent) {
            if (child.name == name) {
                return child;
            }
            innerReturn = child.GetChildByName(name);
            if (innerReturn != null) {
                return innerReturn;
            }
        }
        return null;
    }

    public static void PrintAllChildren(this Transform parent) {
        parent.PrintAllChildren(0);
    }
    private static void PrintAllChildren(this Transform parent, int plane) {
        string s = "";
        for (int i = 0; i < plane; i++) {
            s += "     ";
        }
        if (plane > 0) {
            s += ">";
        }
        foreach (Transform child in parent) {
            Debug.Log(s + child.name);
            child.PrintAllChildren(plane + 1);
        }
    }

    public static void DestroyAllChildren(this Transform transform) {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static T GetComponentAndCheckChildren<T>(this Transform toCheck) where T : UnityEngine.Object {
        T component = toCheck.GetComponent<T>();
        if (component == null) {
            T innerReturn;
            foreach (Transform child in toCheck) {
                innerReturn = child.GetComponentAndCheckChildren<T>();
                if (innerReturn != null) {
                    return innerReturn;
                }
            }
        }
        return component;
    }
    public static List<T> GetAllComponentsIncludingChildren<T>(this Transform toCheck) where T : UnityEngine.Object {
        List<T> toReturn = new List<T>();
        T component = toCheck.GetComponent<T>();
        if (component != null) toReturn.Add(component);
        List<T> innerReturn;
        foreach (Transform child in toCheck) {
            innerReturn = child.GetAllComponentsIncludingChildren<T>();
            if (innerReturn != null) toReturn.AddRange(innerReturn);
        }
        return toReturn;
    }

    public static void ResetToInitial(this Transform toReset, bool forLocal = true) {
        if (forLocal) {
            toReset.localScale = Vector3.one;
            toReset.localRotation = Quaternion.identity;
            toReset.localPosition = Vector3.zero;
        } else {
            toReset.localScale = Vector3.one; // there is only localScale
            toReset.position = Vector3.zero;
            toReset.rotation = Quaternion.identity;
        }
    }
}
