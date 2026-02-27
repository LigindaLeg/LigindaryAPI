using System.Linq;
using MapGeneration;
using UnityEngine;

namespace Ligindary.API.Spawning;

public static class RoomOffsetType
{
    public static readonly RoomOffset Example =
        new RoomOffset(Room.Get(RoomName.Outside).First(), new Vector3(0,0,0), new Quaternion(0,0,0,0));
}