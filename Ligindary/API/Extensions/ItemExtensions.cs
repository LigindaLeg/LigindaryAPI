using System.Linq;

namespace Ligindary.Extensions;

public static class ItemExtensions
{
    /// <summary>
    /// Проверяет, является ли предмет кастомным.
    /// </summary>
    public static bool IsCustomItem(this Item item)
    {
        return Lists.CustomItemsSerials.ContainsValue(item.Serial);
    }
    
    /// <summary>
    /// Получает кастомный предмет.
    /// </summary>
    public static CustomItem? CustomItem(this Item item)
    {
        return (from serial in Lists.CustomItemsSerials where serial.Value == item.Serial select serial.Key).FirstOrDefault();
    }
}