namespace Ligindary;

public class Lists
{
    /// <summary>
    /// Список игроков с выданными видимыми ролями.
    /// </summary>
    public static Dictionary<Player, RoleTypeId> Appearance { get; } = new();
    
    /// <summary>
    /// Список игроков с кастомными эффектами.
    /// </summary>
    public static Dictionary<Player, CustomEffect> PlayerCustomEffects { get; } = new();
    
    /// <summary>
    /// Список кастомных эффектов.
    /// </summary>
    public static List<CustomEffect> CustomEffects { get; } = new();
    
    /// <summary>
    /// Список кастомных предметов.
    /// </summary>
    public static List<CustomItem> CustomItems { get; } = new();
    
    /// <summary>
    /// Список сериалов кастомных предметов.
    /// </summary>
    public static Dictionary<CustomItem, ushort> CustomItemsSerials { get; } = new();
    
    /// <summary>
    /// Список игроков с хинтами.
    /// </summary>
    public static Dictionary<Player, Hint> CIHints { get; } = new();
}