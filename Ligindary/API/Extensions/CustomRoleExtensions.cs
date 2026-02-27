namespace Ligindary.Extensions;

public static class CustomRoleExtensions
{
    /// <summary>
    /// Выдаёт роль игроку.
    /// </summary>
    public static void GiveToPlayer(this CustomRole role, Player player)
    {
        player.GiveCustomRole(role);
    }
    
    /// <summary>
    /// Выдаёт роль всем игрокам.
    /// </summary>
    public static void GiveToAllPlayers(this CustomRole role)
    {
        foreach (var player in Player.List)
        {
            player.GiveCustomRole(role);
        }
    }
    
    /// <summary>
    /// Регистрирует роль.
    /// </summary>
    public static void RegisterRole(CustomRole role)
    {
        Lists.CustomRoles.Add(role);
    }
}