using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IProjectRepository {
        public Task<IEnumerable<Project>> GetAsync(int projectAreaId);
        public Task<Project?> FirstOrDefaultAsync(int projectId);
        public Task CreateAsync(Project project);
        public Task UpdateAsync(Project project);
    }
}