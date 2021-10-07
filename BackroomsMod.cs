using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TheBackrooms.Common.UI;
using TheBackrooms.Core.Assets;
using TheBackrooms.Core.UISystem;

namespace TheBackrooms
{
    public class BackroomsMod : Mod
    {
        public static BackroomsMod Instance => ModContent.GetInstance<BackroomsMod>();

        public UserInterfaceRepository InterfaceRepository = new UserInterfaceRepository();

        public static string GetTranslation(string key) => Language.GetTextValue("Mods.TheBackrooms." + key);

        public override void Load()
        {
            base.Load();

            GeneratedAssets.GenerateAssets();

            InterfaceRepository.Register<TitleUI>();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);

            InterfaceRepository.UpdateUIs(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);

            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new BackroomsInterfaceLayer(
                    "Interfaces",
                    delegate
                    {
                        InterfaceRepository.DrawUIs(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
    }
}