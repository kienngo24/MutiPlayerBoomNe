using UnityEngine;
using static LobbyManager;

[CreateAssetMenu(fileName = "NewRoom",menuName = "Game/Room")]
public class Room : ScriptableObject
{
    public Sprite avatar;
    public GameMode gameMode;
    public int maxPlayers;
    public bool isPrivate;

}
