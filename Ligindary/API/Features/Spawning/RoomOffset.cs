using System.Linq;
using MapGeneration;
using UnityEngine;

namespace Ligindary.API.Spawning;

/// <summary>
/// Используется как конкретная позиция в конкретной комнате.
/// </summary>
public class RoomOffset(Room room, Vector3 localPos, Quaternion localRot)
{
    /// <summary>
    /// Комната оффесета.
    /// </summary>
    public Room Room { get; } = room;
    
    /// <summary>
    /// Позиция в комнате.
    /// </summary>
    public Vector3 localPosition { get; } = localPos;

    /// <summary>
    /// Вращение в позиции.
    /// </summary>
    public Quaternion localRotation { get; } = localRot;
    
    /// <summary>
    /// Получает все оффсеты для комнаты из комнаты.
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public IEnumerable<RoomOffset>? Get(Room room)
    {
        var ret = (from off in Lists.RoomOffsets where off.Room == room select off);
        if (!ret.IsEmpty()) return ret;
        Log.Error($"Room Offset {room.Name}(Get type: Room) could not be found.");
        return null;
    }
    
    /// <summary>
    /// Получает все оффсеты для комнаты из имени комнаты.
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public IEnumerable<RoomOffset>? Get(RoomName room)
    {
        var ret = (from off in Lists.RoomOffsets where off.Room.Name == room select off);
        if (!ret.IsEmpty()) return ret;
        Log.Error($"Room Offset {room}(Get type: RoomName) could not be found.");
        return null;
    }
}