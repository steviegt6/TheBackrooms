using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
using TheBackrooms.Core.Utilities;

namespace TheBackrooms
{
    public class BackroomsMod : Mod
    {
        public static BackroomsMod Instance => ModContent.GetInstance<BackroomsMod>();

        public UserInterfaceRepository InterfaceRepository = new UserInterfaceRepository();

        public ContentLoader Loader { get; private set; }

        public Dictionary<string, SubWorld> Worlds { get; } = new Dictionary<string, SubWorld>();

        public Dictionary<string, Process> PortToProcess = new Dictionary<string, Process>();

        public Dictionary<Process, string> ServerToSubWorld = new Dictionary<Process, string>();

        public Dictionary<string, string> SubWorldToPort = new Dictionary<string, string>();

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
            int swIndex = arguments.IndexOf("-sub-world");

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
                    Logger.Info("Sub-world not specified after -sub-world argument.");
            }
            else
                Logger.Info("No sub-world found as a -sub-world argument as not specified.");
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

        public void SendPacketToPlayer(int recipient, string port)
        {
            using (ModPacket packet = GetPacket())
            {
                // Write port to packet and send to initial sender (now packet recipient).
                packet.Write((int) SyncPacket.ServerToPlayerTransfer);
                packet.Write(port);
                packet.Send(recipient);
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);

            SyncPacket packet = (SyncPacket) reader.ReadInt32();

            switch (packet)
            {
                case SyncPacket.PlayerToServerTransfer:
                    string subWorld = reader.ReadString();
                    IPAddress address = IPAddress.Parse(reader.ReadString());

                    if (!SubWorldToPort.TryGetValue(subWorld, out string port))
                        port = ServerHelper.GetFreeTcpPort(address).ToString();
                    else
                    {
                        SendPacketToPlayer(whoAmI, port);
                        return;
                    }

                    string launchArgs = "-autoshutdown " +
                                        $"-password \"{Netplay.ServerPassword}\" " +
                                        $"-lang {Language.ActiveCulture.LegacyId} " +
                                        $"-sub-world {subWorld} " +
                                        $"-port {port}";

                    if (!string.IsNullOrEmpty(Main.libPath))
                        launchArgs += " -loadlib " + Main.libPath;

                    Process server = new Process
                    {
                        StartInfo = new ProcessStartInfo("tModLoaderServer.exe", launchArgs)
                        {
                            UseShellExecute = false,
                            CreateNoWindow = !Debugger.IsAttached,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        }
                    };

                    server.Start();
                    PortToProcess[port] = server;
                    ServerToSubWorld[server] = subWorld;
                    SubWorldToPort[subWorld] = port;

                    // TODO: NOT SAFE TO SEND PLAYER UNTIL SERVER DONE LOADING!!!
                    SendPacketToPlayer(whoAmI, port);

                    break;

                case SyncPacket.ServerToPlayerTransfer:

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}