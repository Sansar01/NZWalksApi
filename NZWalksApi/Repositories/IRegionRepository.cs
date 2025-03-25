using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllAsync();
    }
}
