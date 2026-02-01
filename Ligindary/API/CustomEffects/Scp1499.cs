using CustomPlayerEffects;
using UnityEngine;

namespace Ligindary.API.CustomEffects;

public class Scp1499 : CustomEffect
{
    public string Name => "Scp1499";

    public string Description => "Эффект SCP-1499 - Противогаз";

    public Vector3 lastPosition;
    
    public void OnGive(Player player, float duration)
    {
        lastPosition = player.GameObject.transform.position;
        player.GameObject.transform.position = new(170f, 304f, 65f);
        player.Health += 20;
        player.Hint("Надет SCP-1499", duration);
        player.EnableEffect<NightVision>(3, duration);
        player.EnableEffect<Slowness>(70, duration);
    }

    public void OnRemove(Player player)
    {
        player.GameObject.transform.position = lastPosition;
    }
}