using Ligindary.API.CustomEffects;

namespace Ligindary;

public class Main : Plugin
{
    public override string Name => "LigindaryAPI";
    public override string Description => "Вспомогательное API для LabAPI-плагинов";
    public override string Author => "liginda";
    public override Version Version => new(1, 0, 0, 0);
    public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
    
    
    public override void Enable()
    {
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
    }

    public override void Disable()
    {
        EventHandlers.UnRegisterEvents();
        Lists.CustomEffects.Clear();
        Lists.PlayerCustomEffects.Clear();
        Lists.Appearance.Clear();
    }
}
