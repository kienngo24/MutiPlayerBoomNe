using Unity.Netcode;
using UnityEngine;


public class PlayerAim : MonoBehaviour
{
    [SerializeField] private float m_speed = 15;
    [SerializeField] private float m_maxDistance = 3;
    public Vector2 m_AimPos;
    public Transform m_PlayerTransform;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool followCamera = true;
    [SerializeField] private bool moveLinear = true;
    private void Start()
    {
        m_PlayerTransform = PlayerController.Instance.transform;

        // IfServerSetActiveFalse();
        Invoke(nameof(SetupCamera), .5f);
    }

    // private void IfServerSetActiveFalse()
    // {
    //     if (NetworkManager.Singleton.IsHost)
    //         gameObject.SetActive(false);
    // }

    private void SetupCamera()
    {
        if(followCamera)
            CameraManager.Instance.SetUpCameraFollow(transform);
        else
            CameraManager.Instance.SetUpCameraFollow(PlayerController.Instance.transform);
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 mousePos = GetMousePosition();
        mousePos.z = 0f;

        Vector3 dir = mousePos - m_PlayerTransform.position;
        float dist = dir.magnitude;

        Vector3 targetPos;
        if (dist > m_maxDistance)
            targetPos = m_PlayerTransform.position + dir.normalized * m_maxDistance;
        else
            targetPos = mousePos;
            
        if(moveLinear)
            MoveLinear(targetPos);
        else
            MoveDynamic(targetPos);

    }
    private void MoveLinear(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.time * m_speed);
    }
    private void MoveDynamic(Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
