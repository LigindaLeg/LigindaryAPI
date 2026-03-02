using System.Linq;
using MapGeneration;
using UnityEngine;

namespace Ligindary.API.Spawning;

public static class RoomOffsetType
{
    public static readonly RoomOffset Gr18 =
        new RoomOffset(Room.Get(RoomName.LczGlassroom).First(), new Vector3(4.3f,1f,2f), new Quaternion(0,0,0,0));
}