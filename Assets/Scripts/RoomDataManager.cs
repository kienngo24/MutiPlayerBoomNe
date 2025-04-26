using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class RoomDataManager : MonoBehaviour
{
    public GameObject prefabRoom;
    public Room[] rooms;

    private void Start() =>  SetupRoom();
    private void SetupRoom()
    {
        foreach (var room in rooms)
        {
            GameObject newRoom =  Instantiate(prefabRoom);
            newRoom.transform.SetParent(transform);
            ListLobbyCreate lobbyCreateScript = newRoom.GetComponent<ListLobbyCreate>();
            lobbyCreateScript.SetUpData(room);
        }
    }

}
