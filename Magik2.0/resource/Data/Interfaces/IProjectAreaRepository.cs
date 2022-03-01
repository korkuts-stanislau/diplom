using Resource.Models;

namespace Resource.Data.Interfaces {
    public interface IProjectAreaRepository {
        public Task<IEnumerable<ProjectArea>> GetAsync(string accountId);
        public Task<ProjectArea?> FirstOrDefaultAsync(int areaId);
        public Task CreateAsync(ProjectArea area);
        public Task UpdateAsync(ProjectArea area);
        public Task DeleteAsync(ProjectArea area);
    }
}