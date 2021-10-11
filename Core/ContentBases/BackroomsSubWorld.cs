using SubworldLibrary;

namespace TheBackrooms.Core.ContentBases
{
    public abstract class BackroomsSubWorld : Subworld
    {
        public abstract BackroomsLevel Level { get; }
    }
}