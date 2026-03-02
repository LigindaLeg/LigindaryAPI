using System.Linq;

namespace Ligindary.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Является ли текст ролью?
    /// </summary>
    public static bool IsRole(this string text) => Enum.GetNames(typeof(RoleTypeId)).Contains(text);
    
    /// <summary>
    /// Конвертирует текст в роль.
    /// </summary>
    public static RoleTypeId ToRole(this string text)
    {
        foreach (RoleTypeId r in Enum.GetValues(typeof(RoleTypeId)))
        {
            if (text == r.ToString()) return r;
        }
        return RoleTypeId.None;
    }

    /// <summary>
    /// Является ли текст кастомным эффектом?
    /// </summary>
    public static bool IsCustomEffect(this string text)
    {
        return Lists.CustomEffects.Any(effect => text == effect.Name);
    }
    
    /// <summary>
    /// Конвертирует текст в кастомный эффект.
    /// </summary>
    public static CustomEffect ToCustomEffect(this string text)
    {
        return Lists.CustomEffects.FirstOrDefault(effect => text == effect.Name);
    }
    
    /// <summary>
    /// Является ли текст кастомным предметом?
    /// </summary>
    public static bool IsCustomItem(this string text)
    {
        return Lists.CustomItems.Any(item => text == item.Name);
    }
    
    /// <summary>
    /// Конвертирует текст в кастомный эффект.
    /// </summary>
    public static CustomItem ToCustomItem(this string text)
    {
        return Lists.CustomItems.FirstOrDefault(item => text == item.Name);
    }
    
    /// <summary>
    /// Является ли текст кастомной ролью?
    /// </summary>
    public static bool IsCustomRole(this string text)
    {
        return Lists.CustomRoles.Any(role => text == role.Name);
    }
    
    /// <summary>
    /// Конвертирует текст в кастомную роль.
    /// </summary>
    public static CustomRole ToCustomRole(this string text)
    {
        return Lists.CustomRoles.FirstOrDefault(role => text == role.Name);
    }
}