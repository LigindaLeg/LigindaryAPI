using Ligindary.API;
using Ligindary.API.CustomEffects;
using Ligindary.API.Features.CustomItems;

namespace Ligindary;

public class Main : Plugin<Config>
{
    public override string Name => "LigindaryAPI";
    public override string Description => "Вспомогательное API для LabAPI-плагинов";
    public override string Author => "liginda";
    public override Version Version => new(1, 1, 0, 0);
    public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
    public static Main Instance;
    
    public override void Enable()
    {
        Instance = this;
        Timing.CallDelayed(10f, delegate()
        {
            ServerConsole.AddLog( "██╗     ██╗ ██████╗ ██╗███╗   ██╗██████╗  █████╗ ██████╗ ██╗   ██╗\n" +
                                    "██║     ██║██╔════╝ ██║████╗  ██║██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝\n" +
                                    "██║     ██║██║  ███╗██║██╔██╗ ██║██║  ██║███████║██████╔╝ ╚████╔╝ \n" +
                                    "██║     ██║██║   ██║██║██║╚██╗██║██║  ██║██╔══██║██╔══██╗  ╚██╔╝  \n" +
                                    "███████╗██║╚██████╔╝██║██║ ╚████║██████╔╝██║  ██║██║  ██║   ██║   \n" +
                                    "╚══════╝╚═╝ ╚═════╝ ╚═╝╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ", 
                                    ConsoleColor.DarkCyan);
        });
        EventHandlers.RegisterEvents();
        CustomEffectExtensions.RegisterEffect(new Scp1499());
        CustomItemExtensions.RegisterItem(new Knife());
        CustomItemExtensions.RegisterItem(new NVG());
        CustomItemExtensions.RegisterItem(new Scp1499Item());
        CustomItemExtensions.RegisterItem(new Stick());
    }

    public override void Disable()
    {
        EventHandlers.UnRegisterEvents();
        Lists.CustomEffects.Clear();
        Lists.PlayerCustomEffects.Clear();
        Lists.Appearance.Clear();
        Lists.CustomItems.Clear();
        Lists.CustomItemsSerials.Clear();
        Instance = null;
    }
}
