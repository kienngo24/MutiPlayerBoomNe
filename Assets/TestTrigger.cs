using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class TestTrigger : NetworkBehaviour
{
    private NetworkVariable<int> m_SomeValue = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Owner);
    [SerializeField] private GameObject effectPrefab;
    private GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
    [ServerRpc]
    public void DestroyServerRpc()
    {
        HideClientRpc();
    }
    [ClientRpc]
    void HideClientRpc()
    {
        effect = ObjectPool.Instance.GetObject(effectPrefab,transform.position,Quaternion.identity);
        ParticleSystem particleSystem= effect.GetComponent<ParticleSystem>();
        particleSystem?.Play();
        gameObject.SetActive(false);
    }
}
