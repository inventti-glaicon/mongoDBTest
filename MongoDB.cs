using MongoDB.Driver;

namespace LanchoneteMongoDB
{
    public static class MongoDB
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;

        public static IMongoDatabase Instance()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("mongodb");

            return _database;
        }
    }
}