using Auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Auth.Data
{
    public class AuthRepository
    {
        private IMongoCollection<Account> accounts;

        public AuthRepository(string connection)
        {
            var mongoConnection = new MongoUrlBuilder(connection);
            MongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase(mongoConnection.DatabaseName);
            accounts = db.GetCollection<Account>("accounts");
        }

        public async Task<Account> GetAccount(string id)
        {
            return await accounts.Find(new BsonDocument("_id", new ObjectId(id)))
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await accounts.Find(new BsonDocument("Email", email))
                .FirstOrDefaultAsync();
        }
        
        public async Task Create(Account a)
        {
            await accounts.InsertOneAsync(a);
        }
        
        public async Task Update(Account a)
        {
            await accounts.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(a.Id)), a);
        }
        
        public async Task Remove(string id)
        {
            await accounts.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
    }
}