namespace Ligindary;

public static class EventHandlers
{
    public static void RegisterEvents()
    {
        FpcServerPositionDistributor.RoleSyncEvent += Events.RoleSyncEvent;
        PlayerEvents.ChangedRole += Events.ChangedRole;
    }
    
    public static void UnRegisterEvents()
    {
        FpcServerPositionDistributor.RoleSyncEvent -= Events.RoleSyncEvent;
        PlayerEvents.ChangedRole -= Events.ChangedRole;
    }

    private static class Events
    {
        public static RoleTypeId RoleSyncEvent(ReferenceHub hub, ReferenceHub otherHub, RoleTypeId role,
            NetworkWriter networkWriter) =>
            Lists.Appearance.ContainsKey(Player.Get(hub)) ? Lists.Appearance[Player.Get(hub)] : role;

        public static void ChangedRole(PlayerChangedRoleEventArgs e) => Lists.Appearance.Remove(e.Player);
    }
}