using System.Net;
using Terraria;
using Terraria.ModLoader;

namespace TheBackrooms.Core.SubWorlds
{
    public sealed class SubWorldManager : ModPlayer
    {
        public SubWorld CurrentWorld { get; internal set; } = null;

        public void TransferRequest(SubWorld world)
        {
            mod.Logger.Debug("Sending server a transfer request to: " + world.Name);

            using (ModPacket packet = mod.GetPacket())
            {
                packet.Write((int) SyncPacket.PlayerToServerTransfer);
                packet.Write(world.Name);
                packet.Write(Netplay.ServerIP.ToString());
                packet.Send();
            }
        }

        public void TransferPlayer(SubWorld world)
        {

        }
    }
}