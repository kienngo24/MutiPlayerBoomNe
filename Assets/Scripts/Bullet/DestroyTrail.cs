using System.Collections;
using JetBrains.Annotations;
using Unity.Netcode;
using UnityEngine;

public class DestroyTrail : NetworkBehaviour
{
    private void Start() {
        if(!IsOwner) return;
        DestroyServerRpc();
    }
    [ServerRpc]
    private void DestroyServerRpc(ServerRpcParams rpcParams = default)
    {
        StartCoroutine(DelayedDespawn());
    }

    private IEnumerator DelayedDespawn()
    {
        yield return new WaitForSeconds(.3f);
        if (this != null && gameObject != null)
        {
            GetComponent<NetworkObject>().Despawn();
        }
    }
}
