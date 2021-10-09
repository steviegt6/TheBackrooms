namespace TheBackrooms
{
    public enum SyncPacket
    {
        // Indicates a player is requesting to transfer sub-worlds.
        PlayerToServerTransfer,

        // Indicates a server has been fully initialized and a player should attempt to join.
        ServerToPlayerTransfer
    }
}