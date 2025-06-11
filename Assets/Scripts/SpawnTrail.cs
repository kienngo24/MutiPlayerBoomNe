using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class SpawnTrail : NetworkBehaviour
{
    [SerializeField] private GameObject trail;
    [SerializeField] private Transform groundPos;
    [SerializeField] private float trailLifetime =.5f;
    private float lastTimeTrail;
    private IMovement movement;
    private void Start() {
        movement = GetComponent<IMovement>();
    }
   
    private void Update() {
        if(!IsOwner) return;
        if(movement.GetPosstion().magnitude <= 0) return;
        
        if(Time.time > lastTimeTrail + trailLifetime)
        {
            SpawnTrailDustRpc(groundPos.position,groundPos.rotation);
            lastTimeTrail = Time.time;
        }
    }
    [Rpc(SendTo.Everyone)]
    private void SpawnTrailDustRpc(Vector3 position, Quaternion rotation)
    {
        GameObject trailToReturn = ObjectPool.Instance.GetObject(trail, position,rotation);
        ObjectPool.Instance.ReturnObject(trailToReturn, 0.3f);
    }
}
