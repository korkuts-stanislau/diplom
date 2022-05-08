using Chat.Data.Interfaces;

namespace Chat.Data.MSImplementations;

public class MSUnitOfWork : IUnitOfWork {
    private readonly AppDbContext context;

    public MSUnitOfWork(AppDbContext context)
    {
        this.context = context;
    }
}