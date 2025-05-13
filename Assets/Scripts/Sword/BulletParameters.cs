using System;
using Unity.Netcode;
using UnityEngine;

public struct BulletParameters : INetworkSerializable
{
    public ulong ownerID;
    public ulong senderID;
    public float speed;
    public byte gunIndex;
    public double startTime;
    public Vector3 startPosition;
    public Quaternion direction;
    public bool IsOwnedByLocalClient;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ownerID);
        serializer.SerializeValue(ref senderID);
        serializer.SerializeValue(ref gunIndex);
        serializer.SerializeValue(ref speed);
        serializer.SerializeValue(ref startTime);
        serializer.SerializeValue(ref startPosition);
        serializer.SerializeValue(ref direction);
        serializer.SerializeValue(ref IsOwnedByLocalClient);

    }
}
