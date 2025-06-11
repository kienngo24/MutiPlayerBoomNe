using Unity.Netcode;
using UnityEngine;

public class SpawnSword : NetworkBehaviour
{
    [SerializeField] private Transform prefab;
    private PlayerAim m_Aim;
    public ISword m_Sword;

        
    public void Start()
    {
        
        if (!IsOwner) return;
        m_Aim = GetComponentInChildren<PlayerAim>();

        SpawnSwordServerRpc();

    }
    [ClientRpc]
    public void RequestSpawnToServerClientRpc(NetworkObjectReference swordRef, ulong clientId)
    {
        if(NetworkManager.Singleton.LocalClientId != clientId) return;
        if (swordRef.TryGet(out NetworkObject swordNetObj))
        {          
            m_Sword = swordNetObj.GetComponentInChildren<ISword>();
            m_Sword?.Setup(m_Aim.transform);
        }
    }
    [ServerRpc]
    private void SpawnSwordServerRpc(ServerRpcParams rpcParams = default)
    {
        Transform swordTransform = Instantiate(prefab);
        swordTransform.position = transform.position; 

        NetworkObject swordNetObj = swordTransform.GetComponent<NetworkObject>();
        swordNetObj.SpawnWithOwnership(rpcParams.Receive.SenderClientId);

        swordTransform.parent = transform;

        var swordRef = new NetworkObjectReference(swordNetObj);
        RequestSpawnToServerClientRpc(swordRef, rpcParams.Receive.SenderClientId);
    }

}
