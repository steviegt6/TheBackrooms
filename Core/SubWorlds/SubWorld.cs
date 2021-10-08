using Terraria.ModLoader;

namespace TheBackrooms.Core.SubWorlds
{
    // TODO: 1.4 ModType
    // TODO: Multi-player.
    public abstract class SubWorld
    {
        public Mod Mod { get; internal set; }

        public virtual string Name => GetType().Name;

        public abstract (int, int) Dimensions { get; }
    }
}