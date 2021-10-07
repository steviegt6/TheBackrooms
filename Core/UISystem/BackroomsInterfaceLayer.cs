using Terraria.UI;

namespace TheBackrooms.Core.UISystem
{
    public class BackroomsInterfaceLayer : LegacyGameInterfaceLayer
    {
        public BackroomsInterfaceLayer(
            string name,
            GameInterfaceDrawMethod drawMethod,
            InterfaceScaleType scaleType = InterfaceScaleType.Game
        ) : base(
            "TheBackrooms:" + name,
            drawMethod,
            scaleType
        )
        {
        }
    }
}