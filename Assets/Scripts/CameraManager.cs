using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [HideInInspector]
    public CinemachineCamera m_CineMachine;
    private void Awake() {
        m_CineMachine = GetComponentInChildren<CinemachineCamera>();
    }
    public void SetUpCameraFollow(Transform follow) => m_CineMachine.Follow = follow;
    
}
