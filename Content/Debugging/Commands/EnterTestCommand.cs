using SubworldLibrary;
using Terraria.ModLoader;
using TheBackrooms.Content.SubWorlds;

namespace TheBackrooms.Content.Debugging.Commands
{
#if DEBUG
    public class EnterTestCommand : ModCommand
    {
        public override string Command => "enter";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Subworld.Enter<TutorialSubWorld>();
        }
    }
#endif
}