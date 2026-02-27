using System.Linq;
using System.Security.Cryptography.Xml;
using HintServiceMeow.Core.Extension;
using Ligindary.API.Features.NBT;

namespace Ligindary.Extensions;

public static class PlayerExtensions
{
    /// <summary>
    /// Настройки видимой роли.
    /// </summary>
    public static Appearance Appearance(this Player player) => new(player);

    /// <summary>
    /// Показывает хинт через HintServiceMeow.
    /// </summary>
    public static Hint Hint(this Player player, string text, float duration, float? y = null, float? x = null)
    {
        // bool hsm = false;
        // foreach (var dep in LabApi.Loader.PluginLoader.Dependencies)
        // {
        //     if (dep.FullName == "HintServiceMeow")
        //     {
        //         hsm = true;
        //     }
        // }
        //
        // if (!hsm)
        // {
        //     Log.Error(
        //         "Для отображения хинтов HintServiceMeow, нужно загрузить HintServiceMeow-LabAPI в папку LabApi/dependencies/global! https://github.com/MeowServer/HintServiceMeow/releases/latest");
        //     return;
        // }

        var hint = new Hint()
        {
            Text = text,
            XCoordinate = x ?? 0,
            YCoordinate = y ?? 700,
        };
        var pd = PlayerDisplay.Get(player);

        
        pd.AddHint(hint);
        Timing.CallDelayed(duration, delegate() { pd.RemoveHint(hint);});
        return hint;
    }

    /// <summary>
    /// Взрывает игрока.
    /// </summary>
    public static void Explode(this Player player, ExplosionType? grenade) =>
        ExplosionUtils.ServerExplode(player.Position, new Footprint(player.ReferenceHub),
            grenade ?? ExplosionType.Grenade);

    /// <summary>
    /// Выдать кастомный эффект игроку.
    /// </summary>
    public static void GiveCustomEffect(this Player player, CustomEffect effect, float duration)
    {
        effect.OnGive(player, duration); 
        Lists.PlayerCustomEffects.Add(player, effect);
        Timing.RunCoroutine(DurationWorkspace.Duration(player, duration, effect));
    }

    /// <summary>
    /// Убрать кастомный эффект игроку.
    /// </summary>
    public static void RemoveCustomEffect(this Player player, CustomEffect effect)
    {
        effect.OnRemove(player);
        Lists.PlayerCustomEffects.Remove(player);
    }
    
    /// <summary>
    /// Убрать все кастомные эффекты игрокам.
    /// </summary>
    public static void RemoveAllCustomEffects()
    {
        foreach (var player in Player.List)
        {
            foreach (var effect in Lists.CustomEffects)
            {
                player.RemoveCustomEffect(effect);
            }
        }
    }
    
    /// <summary>
    /// Выдать кастомный эффект игроку.
    /// </summary>
    public static void GiveCustomItem(this Player player, CustomItem item)
    {
        item.OnGive(player); 
        item.RegisterEvents();
        foreach (var v in Lists.CIHints.Where(v => v.Key == player))
        {
            player.RemoveHint(v.Value);
        }
        Lists.CustomItemsSerials.Add(item, player.AddItem(item.ItemType).Serial);
        Lists.CIHints.Add(player, player.Hint(Main.Instance.Config!.CustomItemGiveHint.Replace("[itemName]", item.Name).Replace("[itemDesc]", item.Description), 5f));
        Timing.CallDelayed(5f, delegate() { Lists.CIHints.Remove(player);});
    }

    /// <summary>
    /// Убрать кастомный предмет игроку.
    /// </summary>
    public static void RemoveCustomItem(this Player player, CustomItem item)
    {
        item.OnRemove(player);
        item.UnregisterEvents();
        foreach (var ci in player.Inventory.UserInventory.Items)
        {
            if (Lists.CustomItemsSerials[item] == ci.Key)
            {
                Item.Get(ci.Key)!.DropItem().Destroy();
            }
        }
    }
    
    /// <summary>
    /// Убрать все кастомные предметы игрокам.
    /// </summary>
    public static void RemoveAllCustomItems()
    {
        foreach (var player in Player.List)
        {
            foreach (var item in Lists.CustomItems)
            {
                player.RemoveCustomItem(item);
            }
        }
    }
    
    /// <summary>
    /// Проверяет, есть ли эффект у игрока.
    /// </summary>
    public static bool HasCustomEffect(this Player player, CustomEffect effect)
    {
        return Lists.PlayerCustomEffects.Any(eff => eff.Key == player && eff.Value == effect);
    }
    
    /// <summary>
    /// Добавляет NBT тэг игроку.
    /// </summary>
    /// <param name="player">Игрок</param>
    /// <param name="key">Имя тэга</param>
    /// <param name="value">Данные тэга</param>
    /// <returns></returns>
    public static bool AddOrSetNbtTag(this Player player, string key, object value)
    {
        if (NbtTagStorage.HasTag(player, key))
            return false;
        NbtTagStorage.AddOrSetTag(player, key, value);
        return true;
    }
    
    /// <summary>
    /// Получает NBT тэг игрока.
    /// </summary>
    /// <param name="player">Игрок</param>
    /// <param name="key">Имя тэга</param>
    /// <typeparam name="T">Тип тэга</typeparam>
    /// <returns></returns>
    public static T GetNbtTag<T>(this Player player, string key)
    {
        return NbtTagStorage.GetTag<T>(player, key);
    }
    
    public static CustomRole GiveCustomRole(this Player player, CustomRole role)
    {
        player.AddOrSetNbtTag("HasCustomRole", true);
        role.OnGive(player);
        role.RegisterEvents();
        player.SetRole(role.RoleType, RoleChangeReason.RemoteAdmin, role.SpawnFlags);
        player.Position = role.SpawnLocation.Room.Room.Position + role.SpawnLocation.Room.localPosition;
        player.Rotation = role.SpawnLocation.Room.localRotation;
        player.Health = role.Health;
        player.MaxHealth = role.Health;
        player.ArtificialHealth = role.AltHealth;
        player.ClearInventory();
        foreach (var i in role.Inventory)
        {
            player.AddItem(i);
        }

        player.CustomInfo = role.CustomInfo;
        player.DisplayName = role.DisplayName;
        return role;
    }

    public static void RemoveCustomRole(this Player player, CustomRole role)
    {
        if (!player.GetNbtTag<bool>("HasCustomRole"))
            return;
        player.AddOrSetNbtTag("HasCustomRole", false);
        role.OnRemove(player);
        role.UnregisterEvents();
        player.SetRole(RoleTypeId.Spectator);
        player.DisplayName = "";
        player.CustomInfo = "";
        player.MaxHealth = 100;
        
    }
}