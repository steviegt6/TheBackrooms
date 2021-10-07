namespace TheBackrooms.Core.Data
{
    public readonly struct LevelDescriptors
    {
        public readonly string One;
        public readonly string Two;
        public readonly string Three;

        public LevelDescriptors(string one, string two, string three)
        {
            One = one;
            Two = two;
            Three = three;
        }
    }
}