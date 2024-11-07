using MongoDB.Driver;

namespace SwiftSend.infrastructure.Context
{
    public class MongoInitializer
    {
        private readonly IMongoDatabase _database;

        public MongoInitializer(IMongoDatabase database)
        {
            _database = database;
        }

        public void EnsureCollectionExists<T>(string collectionName = null)
        {
            collectionName = collectionName ?? typeof(T).Name;

            var collectionList = _database.ListCollectionNames().ToList();

            if (!collectionList.Contains(collectionName))
            {
                _database.CreateCollection(collectionName);
            }
        }
    }
}
