namespace Ligindary.Extensions;

public static class RoleTypeIdExtensions
{
    /// <summary>
    /// Получает имя роли.
    /// </summary>
    public static string Name (this RoleTypeId role) => role.ToString();

    /// <summary>
    /// Проверяет, является ли роль FPC.
    /// </summary>
    public static bool IsFpc(this RoleTypeId role) => role is not (RoleTypeId.Spectator or RoleTypeId.None or RoleTypeId.Filmmaker or RoleTypeId.Overwatch);
}