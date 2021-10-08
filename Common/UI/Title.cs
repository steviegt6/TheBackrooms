using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheBackrooms.Common.UI
{
    public struct Title
    {
        public string Text;
        public List<string> AboveInfo;
        public List<string> BelowInfo;
        public Color TextColor;

        public Title(string text, List<string> aboveInfo, List<string> belowInfo, Color textColor)
        {
            Text = text;
            AboveInfo = aboveInfo;
            BelowInfo = belowInfo;
            TextColor = textColor;
        }
    }
}