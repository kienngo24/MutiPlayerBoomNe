using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameModeSelection_UI : MonoBehaviour
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
            newRoom.transform.localScale = new Vector3(1, 1, 1);
            UILobbyListCreated lobbyCreateScript = newRoom.GetComponent<UILobbyListCreated>();
            lobbyCreateScript.SetUpData(room);
        }
    }

}
