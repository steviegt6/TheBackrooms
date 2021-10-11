using System;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using TheBackrooms.Core.Data;

namespace TheBackrooms.Core.ContentBases
{
    public abstract class BackroomsLevel
    {
        public virtual LevelType Type => LevelType.Normal;

        public virtual LevelClass Classification => LevelClass.One;

        public abstract string DisplayName { get; }

        public abstract LevelDescriptors Descriptors { get; }

        public virtual string TypeString() => string.Format(
            BackroomsMod.GetTranslation("LevelType"),
            BackroomsMod.GetTranslation("LevelTypes." + Type)
        );

        public virtual string ClassificationString() => string.Format(
            BackroomsMod.GetTranslation("ClassType"),
            BackroomsMod.GetTranslation("Classifications." + Classification)
        );

        public virtual Color ClassificationColor()
        {
            switch (Classification)
            {
                case LevelClass.Zero:
                    return Color.LightYellow;

                case LevelClass.One:
                    return Color.Gold;

                case LevelClass.Two:
                    return Color.Goldenrod;

                case LevelClass.Three:
                    return Color.OrangeRed;

                case LevelClass.Four:
                    return Color.Red;

                case LevelClass.Five:
                    return Color.DarkRed;

                case LevelClass.Habitable:
                    return Color.Teal;

                case LevelClass.DeadZone:
                    return Color.Firebrick;

                case LevelClass.Pending:
                    return Color.LightSlateGray;

                case LevelClass.Amended:
                    return Color.Yellow;

                case LevelClass.Omega:
                    return Color.LightSteelBlue;

                case LevelClass.Unknown:
                    return Color.Gray;

                case LevelClass.NotApplicable:
                    return Color.DarkSlateGray;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}