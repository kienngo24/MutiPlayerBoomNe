using System;
using Unity.Netcode;
using UnityEngine;

public class SingletonNetwork<T> : NetworkBehaviour where T : NetworkBehaviour
{
    private static T _singleton;
    private static bool _isQuitting = false;

    public static T Singleton
    {
        get
        {
            if (_isQuitting) return null;

            if (_singleton == null)
                _singleton = FindFirstObjectByType<T>();

            return _singleton;
        }
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (_singleton != null && _singleton != this)
        {
            Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T).Name} found, destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        _singleton = this as T;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
