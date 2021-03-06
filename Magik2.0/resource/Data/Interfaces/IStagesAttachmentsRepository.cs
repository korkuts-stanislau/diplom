using System.Linq.Expressions;
using Resource.Models;

namespace Resource.Data.Interfaces
{
    public interface IStagesAttachmentsRepository {
        public Task<IEnumerable<StageAttachment>> GetAsync(int stageId);
        public Task<StageAttachment?> FirstOrDefaultAsync(int attachmentId);
        public Task<IEnumerable<StageAttachment>> WhereAsync(Expression<Func<StageAttachment, bool>> predicate);
        public Task CreateAsync(StageAttachment attachment);
        public Task UpdateAsync(StageAttachment attachment);
        public Task DeleteAsync(StageAttachment attachment);
    }
}