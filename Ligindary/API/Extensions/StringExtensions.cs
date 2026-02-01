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
}