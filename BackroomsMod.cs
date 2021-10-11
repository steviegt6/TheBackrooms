using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using SubworldLibrary;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using TheBackrooms.Common.UI;
using TheBackrooms.Core.Assets;
using TheBackrooms.Core.Loading;
using TheBackrooms.Core.UISystem;

namespace TheBackrooms
{
    public class BackroomsMod : Mod
    {
        public static BackroomsMod Instance => ModContent.GetInstance<BackroomsMod>();

        public UserInterfaceRepository InterfaceRepository = new UserInterfaceRepository();

        public ContentLoader Loader { get; private set; }

        private Delegate SubWorldCallback;

        public BackroomsMod()
        {
            SubWorldCallback = Delegate.CreateDelegate(typeof(ILContext.Manipulator), GetType(), nameof(OverwriteLoad));
            HookEndpointManager.Modify(
                typeof(SubworldLibrary.SubworldLibrary)
                    .GetMethod(
                        "Load",
                        BindingFlags.Instance | BindingFlags.Public),
                SubWorldCallback
            );
        }

        public static string GetTranslation(string key) => Language.GetTextValue("Mods.TheBackrooms." + key);

        public override void Load()
        {
            Loader = new ContentLoader(Code);

            base.Load();

            GeneratedAssets.GenerateAssets();

            InterfaceRepository.Register<TitleUI>();
        }

        public override void Unload()
        {
            base.Unload();

            HookEndpointManager.Unmodify(
                typeof(SubworldLibrary.SubworldLibrary)
                    .GetMethod(
                        "Load",
                        BindingFlags.Instance | BindingFlags.Public),
                SubWorldCallback
            );

            SubWorldCallback = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);

            InterfaceRepository.UpdateUIs(gameTime);
        }

        public static void OverwriteLoad(ILContext context)
        {
            // ReSharper disable once StringLiteralTypo
            if (!context.Method.DeclaringType.Name.Contains("SubworldLibrary"))
                return;

            ILCursor c = new ILCursor(context);

            c.GotoNext(x => x.MatchLdtoken(out _))
                .GotoNext(x => x.MatchLdtoken(out _))
                .GotoNext(x => x.MatchLdtoken(out _));

            c.RemoveRange(3);

            c.EmitDelegate<Func<Type, bool>>(type => !type.IsAbstract && type.IsSubclassOf(typeof(Subworld)));
        }
    }
}