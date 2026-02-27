using Ligindary.API.Spawning;

namespace Ligindary.API.Features.CustomRoles;

public class ExampleRole : CustomRole
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
    public SpawnLocation SpawnLocation => SpawnLocationType.Ex;
    public int SpawnChance => 50;
    public RoleSpawnFlags SpawnFlags => RoleSpawnFlags.None;
    public void OnGive(Player player)
    {
        Server.RunCommand($"slw suit {player.PlayerId} scp999");
    }

    public void OnRemove(Player player)
    {
        Server.RunCommand($"slw remove {player.PlayerId} scp999");
    }

    public void RegisterEvents()
    {
        
    }

    public void UnregisterEvents()
    {
        
    }
}