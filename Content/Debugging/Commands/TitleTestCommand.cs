using Terraria.ModLoader;
using TheBackrooms.Common.UI;
using TheBackrooms.Core.ContentBases;
using TheBackrooms.Core.Data;

namespace TheBackrooms.Content.Debugging.Commands
{
#if DEBUG
    // Not synced.
    public class TitleTestCommand : ModCommand
    {
        public class TestLevel : BackroomsLevel
        {
            public override string DisplayName => "The Backrooms: Level 1";

            public override LevelDescriptors Descriptors =>
                new LevelDescriptors("Safe", "Secure", "Minimal Entity Count");

            public override LevelClass Classification => LevelClass.One;

            public override LevelType Type => LevelType.Normal;
        }

        public override string Command => "title";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            BackroomsMod.Instance.InterfaceRepository.GetState<TitleUI>().SetLevel(new TestLevel());

            if (!BackroomsMod.Instance.InterfaceRepository.Enabled<TitleUI>())
                BackroomsMod.Instance.InterfaceRepository.ToggleState<TitleUI>();
        }
    }
#endif
}