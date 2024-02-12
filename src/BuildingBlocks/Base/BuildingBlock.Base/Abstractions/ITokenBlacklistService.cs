namespace BuildingBlock.Base.Abstractions
{
    public interface ITokenBlacklistService
    {
        void AddToBlacklist(string token);
        bool IsTokenBlacklisted(string token);

        void ClearBlacklist();

    }
}
