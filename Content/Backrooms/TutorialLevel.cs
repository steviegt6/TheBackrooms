using SubworldLibrary;
using TheBackrooms.Content.SubWorlds;
using TheBackrooms.Core.ContentBases;
using TheBackrooms.Core.Data;

namespace TheBackrooms.Content.Backrooms
{
    public class TutorialLevel : BackroomsLevel
    {
        public override string DisplayName => BackroomsMod.GetTranslation("TutorialLevel");

        public override LevelDescriptors Descriptors => new LevelDescriptors(
            BackroomsMod.GetTranslation("BackroomsDescriptors.Safe"),
            BackroomsMod.GetTranslation("BackroomsDescriptors.Secure"),
            BackroomsMod.GetTranslation("BackroomsDescriptors.MinimalEntityCount")
        );

        public override LevelClass Classification => LevelClass.One;

        public override LevelType Type => LevelType.Normal;
    }
}