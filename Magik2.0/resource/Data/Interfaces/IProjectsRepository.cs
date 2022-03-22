using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IProjectsRepository {
        public Task<IEnumerable<Project>> GetAsync(int fieldId);
        public Task<Project?> FirstOrDefaultAsync(int projectId);
        public Task CreateAsync(Project project);
        public Task UpdateAsync(Project project);
        public Task DeleteAsync(Project project);
    }
}