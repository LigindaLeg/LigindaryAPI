namespace Ligindary.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class MainCommand : ParentCommand
{
    public MainCommand() => LoadGeneratedCommands();
    
    public override string Command => "lapi";

    public override string[] Aliases { get; } = ["liginda", "ligindary"];

    public override string Description => "Главная команда LigindaryAPI";

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new ChangeAppearance());
        RegisterCommand(new CustomEffect.CustomEffect());
        RegisterCommand(new CustomItem.CustomItem());
        RegisterCommand(new CustomRoles.CustomRole());
        RegisterCommand(new GetLocalRoomPosition());
    }
    
    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        string res = "Доступные команды:";
        foreach (var com in AllCommands)
        {
            res += $"\nlapi {com.Command}";
        }
        response = res;
        return false;
    }
}