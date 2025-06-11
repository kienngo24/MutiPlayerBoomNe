using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class ManagerTimeLobby
{
    private float refreshCooldown = 1.1f;
    private int maxCountToRefresh = 2;

    // Dictionary lưu thời gian và số lần refresh của từng client
    private Dictionary<ulong, (float lastRefreshTime, int count) > clientRefreshData = new();

    // Gọi từ client để yêu cầu refresh lobby
    [ServerRpc(RequireOwnership = false)]
    public void RequestRefreshLobbyServerRpc(ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;

        // Lấy data client
        if (!clientRefreshData.TryGetValue(clientId, out var data))
        {
            data = (0f, 0);
        }

        float currentTime = Time.time;

        // Nếu vượt thời gian cooldown, reset đếm
        if (currentTime > data.lastRefreshTime + refreshCooldown)
        {
            data.count = 0;
        }

        data.count++;

        if (data.count <= maxCountToRefresh)
        {
            Debug.Log($"Client {clientId} được phép refresh lobby. Count = {data.count}");

            // ✅ Gửi kết quả về client nếu muốn
            RefreshLobbyClientRpc(true, clientId); // Cho phép refresh
        }
        else
        {
            Debug.LogWarning($"Client {clientId} đã vượt quá giới hạn refresh. Count = {data.count}");

            RefreshLobbyClientRpc(false, clientId); // Không cho phép
        }

        // Cập nhật thời gian
        clientRefreshData[clientId] = (currentTime, data.count);
    }

    // Gửi kết quả lại cho từng client
    [ClientRpc]
    private void RefreshLobbyClientRpc(bool allowed, ulong targetClientId)
    {
        if (NetworkManager.Singleton.LocalClientId != targetClientId)
            return;

        if (allowed)
        {
            Debug.Log("✅ Bạn được phép refresh lobby");
            // Thực hiện hành động refresh tại client
        }
        else
        {
            Debug.LogWarning("❌ Bạn đã vượt quá giới hạn refresh!");
            // Có thể hiển thị popup cho người dùng
        }
    }
}
