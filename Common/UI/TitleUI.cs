using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public Title Title { get; protected set; }

        public float Timer;
        public float LineProgress;
        public float TextProgress;
        public bool Playing;
        public bool Fading;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Playing)
                return;

            if (Timer >= 300)
                Fading = true;

            if (Fading)
                Timer -= 1.75f;
            else
                Timer++;

            if (Timer == 0 && Fading)
                Playing = false;

            LineProgress = (float) Easings.EaseInOutCirc(MathHelper.Clamp(Timer / 100f, 0f, 1f));
            TextProgress = (float) Easings.EaseInOutCirc(MathHelper.Clamp(Timer / 100f - 1f, 0f, 1f));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Playing)
                return;

            base.Draw(spriteBatch);
            Vector2 textSize = Main.fontDeathText.MeasureString(Title.Text);
            Color color = Title.TextColor;
            float mouseFontHeight = Main.fontMouseText.MeasureString("Y").Y;

            #region Bar

            Rectangle destination = new Rectangle(
                Main.screenWidth / 2,
                Main.screenHeight / 4,
                (int)(LineProgress * textSize.X / 2f),
                6
            );

            // Main bar.
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

            // Endings of the bar.
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

            #endregion

            // Text drawing.
            Vector2 drawPos = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 4f);
            drawPos.Y -= 12f * TextProgress - textSize.Y / 2f * TextProgress;
            drawPos.X -= textSize.X / 2f;

            #region Title

            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                Main.fontDeathText,
                Title.Text,
                drawPos,
                color,
                0f,
                new Vector2(0f, textSize.Y),
                new Vector2(1f, TextProgress)
            );

            #endregion

            #region Above Text

            foreach (string above in Title.AboveInfo)
            {
                // Scaling offset
                drawPos.Y -= mouseFontHeight * TextProgress;

                // Render text with squished scale and position offset
                ChatManager.DrawColorCodedStringWithShadow(
                    Main.spriteBatch,
                    Main.fontMouseText,
                    above,
                    drawPos,
                    color,
                    0f,
                    new Vector2(0f, textSize.Y),
                    new Vector2(1f, TextProgress)
                );
            }

            #endregion

            #region Below Text

            // Initial offsets.

            // 1. Find constant relative text position.
            drawPos = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 4f);

            // 2. Offset y-axis by half-size, scale with text progress/
            drawPos.Y -= textSize.Y / 2f * TextProgress;

            // 3. Offset x-axis by half-size, no scaling.
            drawPos.X -= textSize.X / 2f;

            // 4. Offset of 16 pixels scaled by text progress. Arbitrary.
            drawPos.Y += 16f * TextProgress;

            foreach (string below in Title.BelowInfo)
            {
                // Offset by text size (scaled).
                drawPos.Y += mouseFontHeight * TextProgress;

                // Draw string.
                ChatManager.DrawColorCodedStringWithShadow(
                    Main.spriteBatch,
                    Main.fontMouseText,
                    below,
                    drawPos,
                    color,
                    0f,
                    Vector2.Zero,
                    new Vector2(1f, TextProgress)
                );
            }

            #endregion
        }

        public void FromLevel(BackroomsLevel level) => SetTitle(new Title(
            level.DisplayName,
            new List<string>
            {
                level.ClassificationString(),
                level.TypeString()
            },
            new List<string>
            {
                level.Descriptors.One,
                level.Descriptors.Two,
                level.Descriptors.Three
            }, level.ClassificationColor()
        ));

        public void SetTitle(Title title)
        {
            Title = title;
            PlayAnimation();
        }

        public void Reset()
        {
            TextProgress = 0f;
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