using System.ComponentModel;

namespace Ligindary.API;

public class Config
{
    [Description("CustomItems Translations / CustomItem Gived")]
    public string CustomItemGiveHint { get; set; } = "Вы получили [itemName].\n[itemDesc]";
    
    [Description("CustomItems Translations / CustomItem Selected")]
    public string CustomItemSelectHint { get; set; } = "Вы взяли [itemName].\n[itemDesc]";
    
    [Description("CustomRoles Translations / CustomRole Spawned")]
    public string CustomRoleSpawnedHint { get; set; } = "Вы были заспавнены за [roleName].\n[roleDesc]";
    
    [Description("CustomRoles Settings / SCP999")]
    public bool IsSCP999Enabled { get; set; } = true;
}