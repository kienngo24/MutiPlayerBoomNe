using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    [SerializeField] private Button btnServer;
    [SerializeField] private Button btnHost;
    [SerializeField] private Button btnClient;
    private void Start() {
        btnServer.onClick.AddListener(() => 
        {
            StartServer();
            Hide();
        });
        btnHost.onClick.AddListener(() => 
        {
            StartHost();
            Hide();
        });
        btnClient.onClick.AddListener(() => 
        {
            StartClient();
            Hide();
        });
    }
    private void StartServer() => NetworkManager.Singleton.StartServer();
    private void StartHost() => NetworkManager.Singleton.StartHost();
    private void StartClient() => NetworkManager.Singleton.StartClient();
    private void Hide() => gameObject.SetActive(false);
    private void Show() => gameObject.SetActive(true);
}
