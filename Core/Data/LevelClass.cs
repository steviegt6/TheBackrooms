namespace TheBackrooms.Core.Data
{
    public enum LevelClass
    {
        // Standard
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,

        // Special classifications
        Habitable = 100,
        DeadZone = 101,
        Pending = 102,
        Amended = 103,
        Omega = 104,

        // N/A + Unknown
        Unknown = 200,
        NotApplicable = 201
    }
}