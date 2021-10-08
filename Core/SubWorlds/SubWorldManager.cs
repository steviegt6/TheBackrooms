using Terraria.ModLoader;

namespace TheBackrooms.Core.SubWorlds
{
    public sealed class SubWorldManager : ModPlayer
    {
        public SubWorld CurrentWorld { get; internal set; } = null;
    }
}