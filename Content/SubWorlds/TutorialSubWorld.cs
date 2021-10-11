using System.Collections.Generic;
using SubworldLibrary;
using Terraria.World.Generation;
using TheBackrooms.Content.Backrooms;
using TheBackrooms.Core.ContentBases;

namespace TheBackrooms.Content.SubWorlds
{
    public class TutorialSubWorld : BackroomsSubWorld
    {
        public override int width => 100;

        public override int height => 100;

        public override List<GenPass> tasks => new List<GenPass>();

        public override BackroomsLevel Level => new TutorialLevel();
    }
}