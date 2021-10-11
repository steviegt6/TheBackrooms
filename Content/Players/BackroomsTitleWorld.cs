using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;
using TheBackrooms.Common.UI;
using TheBackrooms.Content.Debugging.Commands;
using TheBackrooms.Core.ContentBases;

namespace TheBackrooms.Content.PLayers
{
    public class BackroomsTitleWorld : ModPlayer
    {
        public override void OnEnterWorld(Player player)
        {
            base.OnEnterWorld(player);

            if (!(SLWorld.currentSubworld is BackroomsSubWorld backWorld) || Main.dedServ)
                return;

            BackroomsMod.Instance.InterfaceRepository.GetState<TitleUI>().FromLevel(backWorld.Level);

            if (!BackroomsMod.Instance.InterfaceRepository.Enabled<TitleUI>())
                BackroomsMod.Instance.InterfaceRepository.ToggleState<TitleUI>();
        }
    }
}