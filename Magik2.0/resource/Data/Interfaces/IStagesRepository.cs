using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IStagesRepository {
        public Task<IEnumerable<Stage>> GetAsync(int projectId);
        public Task<Stage?> FirstOrDefaultAsync(int stageId);
        public Task CreateAsync(Stage stage);
        public Task UpdateAsync(Stage stage);
        public Task DeleteAsync(Stage stage);
    }
}