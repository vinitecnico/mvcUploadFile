using mvcUploadFile.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mvcUploadFile.Data {
    public class Context {
        private readonly IMongoDatabase _database = null;

        public Context (IOptions<Settings> settings) {
            var client = new MongoClient (settings.Value.ConnectionString);
            if (client != null) {
                _database = client.GetDatabase (settings.Value.Database);
            }
        }

        public IMongoCollection<Note> Notes {
            get {
                return _database.GetCollection<Note> ("pointSheet");
            }
        }
    }
}