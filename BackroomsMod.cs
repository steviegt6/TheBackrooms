using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TheBackrooms.Common.UI;
using TheBackrooms.Core.Assets;
using TheBackrooms.Core.Loading;
using TheBackrooms.Core.SubWorlds;
using TheBackrooms.Core.UISystem;

namespace TheBackrooms
{
    public class BackroomsMod : Mod
    {
        public static BackroomsMod Instance => ModContent.GetInstance<BackroomsMod>();

        public UserInterfaceRepository InterfaceRepository = new UserInterfaceRepository();

        public ContentLoader Loader { get; private set; }

        public Dictionary<string, SubWorld> Worlds { get; } = new Dictionary<string, SubWorld>();

        public static string GetTranslation(string key) => Language.GetTextValue("Mods.TheBackrooms." + key);

        public override void Load()
        {
            Loader = new ContentLoader(Code);

            base.Load();

            GeneratedAssets.GenerateAssets();

            InterfaceRepository.Register<TitleUI>();

            foreach (SubWorld subWorld in Loader.OfInstances<SubWorld>())
                Worlds.Add(subWorld.Name, subWorld);
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();

            List<string> arguments = Environment.GetCommandLineArgs().ToList();
            int swIndex = arguments.IndexOf("--sub-world");

            if (swIndex != -1)
            {
                if (arguments.Count > swIndex + 1)
                {
                    string subWorld = arguments[swIndex];

                    if (Worlds.TryGetValue(subWorld, out SubWorld world))
                        ModContent.GetInstance<SubWorldManager>().CurrentWorld = world;
                    else
                        Logger.Error("No sub-world found with the name: " + subWorld);
                }
                else
                    Logger.Info("Sub-world not specified after --sub-world argument.");
            }
            else
                Logger.Info("No sub-world found as a --sub-world argument as not specified.");
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