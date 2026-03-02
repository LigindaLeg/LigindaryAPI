using System.Linq;
using HintServiceMeow.Core.Extension;
using Ligindary.API.Features.NBT;

namespace Ligindary;

public static class EventHandlers
{
    public static void RegisterEvents()
    {
        FpcServerPositionDistributor.RoleSyncEvent += Events.RoleSyncEvent;
        PlayerEvents.ChangedRole += Events.ChangedRole;
        PlayerEvents.ChangedItem += Events.ChangedItem;
        PlayerEvents.Joined += Events.Joined;
        PlayerEvents.Left += Events.Left;
        ServerEvents.RoundStarted += Events.RoundStarted;
    }
    
    public static void UnRegisterEvents()
    {
        FpcServerPositionDistributor.RoleSyncEvent -= Events.RoleSyncEvent;
        PlayerEvents.ChangedRole -= Events.ChangedRole;
        PlayerEvents.ChangedItem -= Events.ChangedItem;
        PlayerEvents.Joined -= Events.Joined;
        PlayerEvents.Left -= Events.Left;
        ServerEvents.RoundStarted -= Events.RoundStarted;
    }

    private static class Events
    {
        public static RoleTypeId RoleSyncEvent(ReferenceHub hub, ReferenceHub otherHub, RoleTypeId role,
            NetworkWriter networkWriter) =>
            Lists.Appearance.ContainsKey(Player.Get(hub)) ? Lists.Appearance[Player.Get(hub)] : role;

        public static void ChangedRole(PlayerChangedRoleEventArgs e)
        {
            Lists.Appearance.Remove(e.Player);
            if (!NbtTagStorage.GetTag<bool>(e.Player, "HasCustomRole"))
                return;
            e.Player.RemoveCustomRole(Lists.PlayerCustomRoles[e.Player], false);
        }
        
        public static void ChangedItem(PlayerChangedItemEventArgs e)
        {
            if (e.NewItem == null)
                return;
            if (!e.NewItem.IsCustomItem())
                return;
            foreach (var v in Lists.CIHints.Where(v => v.Key == e.Player))
            {
                e.Player.RemoveHint(v.Value);
            }
            Lists.CIHints.Remove(e.Player);
            Lists.CIHints.Add(e.Player, e.Player.Hint(Main.Instance.Config!.CustomItemSelectHint.Replace("[itemName]", e.NewItem!.CustomItem()!.Name).Replace("[itemDesc]", e.NewItem!.CustomItem()!.Description), 10f));
            Timing.CallDelayed(5f, delegate() { Lists.CIHints.Remove(e.Player);});
        }

        public static void Joined(PlayerJoinedEventArgs e)
        {
            if (e.Player == null)
                return;
            NbtTagStorage.AddOrSetTag(e.Player, "HasCustomRole", false);
        }

        public static void Left(PlayerLeftEventArgs e)
        {
            if (e.Player == null)
                return;
            NbtTagStorage.Clear(e.Player);
        }

        public static void RoundStarted()
        {
            foreach (var d in Door.List)
            {
                d.IsOpened = true;
                d.IsOpened = false;
            }
            Timing.CallDelayed(1f, delegate()
            {
                foreach (var r in Ligindary.Lists.CustomRoles)
                {
                    if (r.SpawnChance <= UnityEngine.Random.Range(0, 100))
                    {
                        Player.ReadyList.ElementAt(UnityEngine.Random.Range(0, Player.ReadyList.Count()))
                            .GiveCustomRole(r);
                    }
                }
            });
        }
    }
}