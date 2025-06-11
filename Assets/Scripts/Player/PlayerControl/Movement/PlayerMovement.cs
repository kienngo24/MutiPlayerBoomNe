using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour, IMovement
{
    private Vector2 m_Direction;  
    [SerializeField] private Vector2 m_MoveDir;
    [SerializeField] private float m_velocity;
    private void Update() 
    {
        Move();
    }
    public void Move()
    {
        if(IsOwner)
        {
            float xPos = Input.GetAxisRaw("Horizontal");
            float yPos = Input.GetAxisRaw("Vertical");

            m_Direction = new Vector2(xPos, yPos).normalized;
            m_MoveDir = m_Direction * m_velocity * Time.deltaTime;

            transform.position += new Vector3(m_MoveDir.x, m_MoveDir.y, 0);
        }
    }
    public Vector2 GetPosstion() => m_Direction;
}
