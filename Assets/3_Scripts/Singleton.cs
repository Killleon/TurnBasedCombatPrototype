using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            if (_instance == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }
}
