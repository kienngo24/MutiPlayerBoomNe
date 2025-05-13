using Unity.Netcode;
using UnityEngine;

public class TestTrigger : NetworkBehaviour
{
    private NetworkVariable<int> m_SomeValue = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ xử lý trên server để tránh lỗi sync
        if (!IsServer) return;

        Debug.Log("Trigger detected on server");
        DestroyServerRpc();
    }
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        m_SomeValue.OnValueChanged += (int old, int newValue) =>
        {
            Debug.Log(NetworkManager.Singleton.LocalClientId + " " + newValue);
        };
        
    }
    void Update()
    {
        if(IsOwner)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                m_SomeValue.Value += 1;
        }
        
    }

    [ServerRpc]
    public void DestroyServerRpc()
    {
        HideClientRpc();
    }
    [ClientRpc]
    void HideClientRpc()
    {
        gameObject.SetActive(false); // Hoặc thực hiện hiệu ứng "biến mất"
    }
}
