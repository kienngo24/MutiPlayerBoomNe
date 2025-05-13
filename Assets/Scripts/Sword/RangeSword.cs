using System;
using Unity.Netcode;
using UnityEngine;


public class RangeSword : NetworkBehaviour, ISword
{
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private Transform pointToSpawn;

    [SerializeField]
    private float trailLifetime = 1.5f;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_FireRate;
    [SerializeField] private float lastTimerFire;
    private Transform follow;
    private ulong ownerClientId;
    private BulletParameters bulletParameters;
    private void Start() {
        ownerClientId = NetworkManager.Singleton.LocalClientId;

    }
    public void Setup(Transform target)
    {
        follow = target;
    }
    private void Update() {
        if(!IsOwner) return;
        
        Rotation();
        Shoot();
    }

    private void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Time.time > lastTimerFire + m_FireRate)
            {
                bulletParameters = new BulletParameters();
                bulletParameters.startPosition = pointToSpawn.position;
                bulletParameters.direction = transform.rotation;
                bulletParameters.ownerID = ownerClientId;
                bulletParameters.senderID = NetworkObjectId;


                SpawnTrailServerRpc(bulletParameters);
                lastTimerFire = Time.time;
            }
        }
    }
    [Rpc(SendTo.Everyone)]
    public void SpawnTrailServerRpc(BulletParameters bulletParameters)
    {
        SpawnBullet(bulletParameters);
    }

    private void SpawnBullet(BulletParameters bulletParameters)
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab,bulletParameters.startPosition,bulletParameters.direction);
        ObjectPool.Instance.ReturnObject(bullet,3f);
    }

    public void Rotation()
    {
        if(follow == null)
            return;
        Vector2 direction = (follow.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
