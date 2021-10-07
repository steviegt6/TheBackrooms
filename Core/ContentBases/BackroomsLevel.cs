using System;
using TheBackrooms.Core.Data;

namespace TheBackrooms.Core.ContentBases
{
    public abstract class BackroomsLevel
    {
        public virtual LevelType Type => LevelType.Normal;

        public virtual LevelClass Classification => LevelClass.One;

        public abstract string DisplayName { get; }

        public abstract LevelDescriptors Descriptors { get; }

        public virtual string TypeString() => BackroomsMod.GetTranslation("LevelTypes." + Type);

        public virtual string ClassificationString() => BackroomsMod.GetTranslation("Classifications." + Classification);
    }
}