using Ligindary.API.Features.NBT;
using Ligindary.API.Spawning;
using UnityEngine;

namespace Ligindary.API.Features.CustomRoles;

public class Scp999 : CustomRole
{
    public string Name => "SCP-999";
    public string Description => "Желейный Монстр";
    public string DisplayName => "SCP-999";

    public string CustomInfo =>
        "<color=#FFFFFF><size=14>Желейный монстр, любит сладкое, оно его бодрит. НИКОГДА НЕ ДАВАЙТЕ ЕМУ КОЛУ!</size></color>";
    public RoleTypeId RoleType => RoleTypeId.Tutorial;
    public ushort Health => 1000;
    public ushort MaxHealth => 1000;
    public ItemType[] Inventory => [];
    public ushort AltHealth => 100;
    public SpawnLocation SpawnLocation => SpawnLocationType.GR18;
    public int SpawnChance => 50;
    public RoleSpawnFlags SpawnFlags => RoleSpawnFlags.None;
    public Vector3 Size => new(0.1f, 0.1f, 0.1f);
    public void OnGive(Player player)
    {
        Server.RunCommand($"/slw suit {player.PlayerId} scp999");
        NbtTagStorage.AddOrSetTag(player, "IsSCP999", true);
        
    }

    public void OnRemove(Player player)
    {
        Server.RunCommand($"/slw remove {player.PlayerId} scp999");
        NbtTagStorage.RemoveTag(player, "IsSCP999");
    }

    public void RegisterEvents()
    {
        PlayerEvents.PickingUpItem += PickingUpItem;
        PlayerEvents.PickingUpScp330 += PickingUpScp330;
    }

    public void UnregisterEvents()
    {
        PlayerEvents.PickingUpItem -= PickingUpItem;
        PlayerEvents.PickingUpScp330 -= PickingUpScp330;
    }

    private static void PickingUpItem(PlayerPickingUpItemEventArgs e)
    {
        if (!NbtTagStorage.HasTag(e.Player, "IsSCP999"))
            return;
        if (e.Pickup is (AmmoPickup or FirearmPickup or MicroHIDPickup or JailbirdPickup))
            e.IsAllowed = false;
    }
    private static void PickingUpScp330(PlayerPickingUpScp330EventArgs e)
    {
        if (NbtTagStorage.HasTag(e.Player, "IsSCP999"))
            e.IsAllowed = false;
    }
}