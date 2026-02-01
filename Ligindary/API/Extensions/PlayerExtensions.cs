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
    public static void Hint(this Player player, string text, float duration, float? y = null, float? x = null)
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
        Timing.CallDelayed(duration, delegate() { pd.RemoveHint(hint); });
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
    
}