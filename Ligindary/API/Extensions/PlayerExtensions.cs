using System.Linq;
using System.Security.Cryptography.Xml;
using HintServiceMeow.Core.Extension;
using Ligindary.API.Features.NBT;
using UnityEngine;

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
        Lists.CIHints.Add(player, player.Hint(Main.Instance.Config!.CustomItemGiveHint.Replace("[itemName]", item.Name).Replace("[itemDesc]", item.Description), 10f));
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
    
    public static CustomRole GiveCustomRole(this Player player, CustomRole role)
    {
        player.SetRole(role.RoleType, RoleChangeReason.RemoteAdmin, role.SpawnFlags);
        NbtTagStorage.AddOrSetTag(player, "HasCustomRole", true);
        Lists.PlayerCustomRoles.Add(player, role);
        var roomTransform = role.SpawnLocation.Room.Room.Transform;
        player.Position = roomTransform.TransformPoint(role.SpawnLocation.Room.localPosition);
        player.Rotation = roomTransform.rotation * role.SpawnLocation.Room.localRotation;
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
        player.Scale = role.Size;
        foreach (var v in Lists.CIHints.Where(v => v.Key == player))
        {
            player.RemoveHint(v.Value);
        }
        Lists.CIHints.Add(player, player.Hint(Main.Instance.Config!.CustomRoleSpawnedHint.Replace("[roleName]", role.Name).Replace("[roleDesc]", role.Description), 15f));
        Timing.CallDelayed(5f, delegate() { Lists.CIHints.Remove(player);});
        role.OnGive(player);
        role.RegisterEvents();
        return role;
    }

    public static void RemoveCustomRole(this Player player, CustomRole role, bool forceToSpectator = true)
    {
        foreach (var v in Lists.CIHints.Where(v => v.Key == player))
        {
            player.RemoveHint(v.Value);
        }
        if (!NbtTagStorage.GetTag<bool>(player, "HasCustomRole"))
            return;
        NbtTagStorage.AddOrSetTag(player, "HasCustomRole", false);
        if (forceToSpectator)
        {
            player.SetRole(RoleTypeId.Spectator);
        }
        player.DisplayName = "";
        player.CustomInfo = "";
        player.MaxHealth = 100;
        player.Scale = new Vector3(1, 1, 1);
        Lists.PlayerCustomRoles.Remove(player);
        role.OnRemove(player);
        role.UnregisterEvents();
    }
}