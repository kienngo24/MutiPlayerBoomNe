using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float trailLifetime = .3f;
    [SerializeField] private float bulletSpeed = 50;
    [SerializeField] private float startTimeBtwTrail =0.04f;
    public BulletParameters bulletParameters;
    private float lastTimeBtwTrail;

    private void FixedUpdate()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
        bulletParameters.startPosition = transform.position;
        if (Time.time > lastTimeBtwTrail + startTimeBtwTrail)
        {
            SpawnTrailRpc();
            lastTimeBtwTrail = Time.time;
        }
        Quaternion rotation = bulletParameters.direction;
        Vector3 direction = rotation * Vector3.right; 
        if(Physics.SphereCast(bulletParameters.startPosition, 1, direction, out RaycastHit hit, 10f))
        {
            TestTrigger networkObject = hit.transform.GetComponent<TestTrigger>();
            if(networkObject != null)
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    networkObject.DestroyServerRpc();
                }
            }
        }       

    }
    private void OnTriggerEnter2D(Collider2D other) {
        ObjectPool.Instance.ReturnObject(gameObject);
    }


    public void SpawnTrailRpc()
    {
        GameObject trailToReturn = ObjectPool.Instance.GetObject(trailPrefab, transform.position, Quaternion.identity);
        ObjectPool.Instance.ReturnObject(trailToReturn, 0.3f);
    }
    
}
