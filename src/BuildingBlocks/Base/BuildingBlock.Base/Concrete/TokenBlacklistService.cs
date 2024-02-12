using BuildingBlock.Base.Abstractions;

namespace BuildingBlock.Base.Concrete
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly List<string> _blacklist;

        public TokenBlacklistService()
        {
            _blacklist = new List<string>();
        }

        public void AddToBlacklist(string token)
        {
            _blacklist.Add(token);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _blacklist.Contains(token);
        }

        public void ClearBlacklist()
        {
            _blacklist.Clear();
        }
    }
}
