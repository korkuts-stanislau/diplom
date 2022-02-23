using Auth.Data.Interfaces;
using Auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Auth.Data.Implementations
{
    public class MongoAccountRepository : IAccountRepository
    {
        private IMongoCollection<Account> accounts;

        public MongoAccountRepository(string connection)
        {
            var mongoConnection = new MongoUrlBuilder(connection);
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase(mongoConnection.DatabaseName);
            accounts = db.GetCollection<Account>("accounts");
        }

        public async Task<Account> GetByIdAsync(string id)
        {
            return await accounts.Find(new BsonDocument("_id", new ObjectId(id)))
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await accounts.Find(new BsonDocument("Email", email))
                .FirstOrDefaultAsync();
        }
        
        public async Task CreateAsync(Account a)
        {
            await accounts.InsertOneAsync(a);
        }
    }
}