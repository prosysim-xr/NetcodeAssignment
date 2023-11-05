using Unity.Netcode;
using UnityEngine;
public struct PlayerOrientation : INetworkSerializable{
    public Vector3 position;
    public Quaternion rotation;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        if (serializer.IsReader) {
            // Now de-serialize the non-complex type properties
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out position);
            reader.ReadValueSafe(out rotation);
        } else {
            // Now serialize the non-complex type properties
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(position);
            writer.WriteValueSafe(rotation);
        }
    }
}