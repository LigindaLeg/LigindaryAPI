using System.Linq;

namespace Ligindary.Extensions;

public static class PickupExtensions
{
    /// <summary>
    /// Проверяет, является ли предмет кастомным.
    /// </summary>
    public static bool IsCustomItem(this Pickup item)
    {
        return Lists.CustomItemsSerials.ContainsValue(item.Serial);
    }
    
    /// <summary>
    /// Получает кастомный предмет.
    /// </summary>
    public static CustomItem? CustomItem(this Pickup item)
    {
        return (from serial in Lists.CustomItemsSerials where serial.Value == item.Serial select serial.Key).FirstOrDefault();
    }
}