using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtentions
{
    /// <summary>
    /// Get a component, log an error if it's not there
    /// </summary>
    /// <typeparam name="T">The type of component to get</typeparam>
    /// <param name="self"></param>
    /// <returns>The component, if found</returns>
    public static T GetComponentRequired<T>(this GameObject self) where T : Component {
        T component = self.GetComponent<T>();

        if (component == null) Debug.LogError("Could not find " + typeof(T) + " on " + self.name);

        return component;
    }

    /// <summary>
    /// Take an action only if a component exists, error if it's not there
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="callback"></param>
    /// <returns>The component, if found</returns>
    public static T GetComponentRequired<T>(this GameObject self, System.Action<T> callback) where T : Component {
        var component = self.GetComponentRequired<T>();

        if (component != null) {
            callback.Invoke(component);
        }

        return component;
    }


    /// <summary>
    /// Get a component, take a different action if it isn't there
    /// </summary>
    /// <typeparam name="T">Component Type</typeparam>
    /// <param name="self">object being extended</param>
    /// <param name="success">Take this action if the component exists</param>
    /// <param name="failure">Take this action if the component does not exist</param>
    /// <returns></returns>
    public static T GetComponent<T>(this GameObject self, System.Action<T> success, System.Action failure) where T : Component {
        var component = self.GetComponent<T>();

        if (component != null) {
            success.Invoke(component);
            return component;
        } else {
            failure.Invoke();
            return null;
        }
    }
    public static T GetComponentAndCheckChildren<T>(this GameObject toCheck) where T : UnityEngine.Object {
        return toCheck.transform.GetComponentAndCheckChildren<T>();
    }
    public static List<T> GetAllComponentsIncludingChildren<T>(this GameObject toCheck) where T : UnityEngine.Object {
        return toCheck.transform.GetAllComponentsIncludingChildren<T>();
    }

}
