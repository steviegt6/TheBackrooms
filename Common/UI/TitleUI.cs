using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.UI.Chat;
using TheBackrooms.Core.Assets;
using TheBackrooms.Core.ContentBases;
using TheBackrooms.Core.Data;
using TheBackrooms.Core.Utilities;

namespace TheBackrooms.Common.UI
{
    // This is simply a container for BackroomsLevel title displays.
    public class TitleUI : UIState
    {
        public BackroomsLevel Level { get; protected set; }

        public int Timer;
        public float LineProgress;
        public bool Playing;
        public bool Fading;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Playing)
                return;

            if (Timer >= 180)
                Fading = true;

            if (Fading)
                Timer--;
            else
                Timer++;

            if (Timer == 0 && Fading)
                Playing = false;

            LineProgress = (float) Easings.EaseInOutCirc(MathHelper.Clamp(Timer / 100f, 0f, 1f));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Playing)
                return;

            base.Draw(spriteBatch);
            Vector2 textSize = Main.fontDeathText.MeasureString(Level.DisplayName);
            Color color = Level.ClassificationColor();

            Rectangle destination = new Rectangle(
                Main.screenWidth / 2,
                Main.screenHeight / 4,
                (int) (LineProgress * textSize.X / 2f),
                6
            );

            // Bar drawing.
            spriteBatch.Draw(GeneratedAssets.LineMiddle, destination, color);
            spriteBatch.Draw(
                GeneratedAssets.LineMiddle,
                destination,
                null,
                color,
                MathHelper.Pi,
                new Vector2(0f, destination.Height),
                SpriteEffects.None,
                0f
            );
            spriteBatch.Draw(
                GeneratedAssets.LineEnding,
                new Vector2(destination.X + destination.Width, destination.Y),
                color
            );
            spriteBatch.Draw(
                GeneratedAssets.LineEnding,
                new Vector2(destination.X - destination.Width - 2f, destination.Y),
                color
            );

            // Text drawing.
            Vector2 drawPos = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 4f);
            drawPos.Y -= (12f * LineProgress) - (textSize.Y / 2f) * LineProgress;
            drawPos.X -= textSize.X / 2f;

            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                Main.fontDeathText,
                Level.DisplayName,
                drawPos,
                color,
                0f,
                new Vector2(0f, textSize.Y),
                new Vector2(1f, LineProgress)
            );

            float minusY = Main.fontMouseText.MeasureString("Y").Y;

            drawPos.Y -= minusY * LineProgress;
            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                Main.fontMouseText,
                string.Format(BackroomsMod.GetTranslation("ClassType"), Level.ClassificationString()),
                drawPos,
                color,
                0f,
                new Vector2(0f, textSize.Y),
                new Vector2(1f, LineProgress)
            );

            drawPos.Y -= minusY * LineProgress;
            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                Main.fontMouseText,
                string.Format(BackroomsMod.GetTranslation("LevelType"), Level.TypeString()),
                drawPos,
                color,
                0f,
                new Vector2(0f, textSize.Y),
                new Vector2(1f, LineProgress)
            );

            // Extra descriptors.
            LevelDescriptors descriptors = Level.Descriptors;
            drawPos = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 4f);
            drawPos.Y -= (textSize.Y / 2f) * LineProgress;
            drawPos.X -= textSize.X / 2f;

            drawPos.Y += 16f* LineProgress;
            drawPos.Y += minusY * LineProgress;
            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                Main.fontMouseText,
                descriptors.One,
                drawPos,
                color,
                0f,
                Vector2.Zero,
                new Vector2(1f, LineProgress)
            );

            drawPos.Y += minusY * LineProgress;
            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                Main.fontMouseText,
                descriptors.Two,
                drawPos,
                color,
                0f,
                Vector2.Zero,
                new Vector2(1f, LineProgress)
            );

            drawPos.Y += minusY * LineProgress;
            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                Main.fontMouseText,
                descriptors.Three,
                drawPos,
                color,
                0f,
                Vector2.Zero,
                new Vector2(1f, LineProgress)
            );
        }

        public void SetLevel(BackroomsLevel level)
        {
            Level = level;
            PlayAnimation();
        }

        public void Reset()
        {
            LineProgress = 0f;
            Timer = 0;
        }

        public void PlayAnimation()
        {
            Reset();

            Playing = true;
            Fading = false;
        }
    }
}