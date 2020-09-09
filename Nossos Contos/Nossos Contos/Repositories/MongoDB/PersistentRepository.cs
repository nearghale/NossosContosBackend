using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nossos_Contos.Model.MongoDB;


namespace Nossos_Contos.Repositories.MongoDB
{
    public class PersistentRepository<T> where T : Entities.MongoDB.Base
    {
        private readonly IMongoCollection<T> Collection;
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;

        public PersistentRepository(IMongoDatabase database, string collection)
        {

            _database = database;
            this.Collection = _database.GetCollection<T>(collection);
        }

        public PersistentRepository(DatabaseSettings settings, string collection)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            this.Collection = _database.GetCollection<T>(collection);

        }

        public List<T> Get() {
            return this.Collection.Find(f => true).ToList();

       }
        public List<T> Find(Expression<Func<T, bool>> expression) { 
            
            return this.Collection.Find(expression).ToList();

        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return this.Collection.Find(expression).FirstOrDefault();
        }

        public long Count(Expression<Func<T, bool>> expression)
        {

            return this.Collection.CountDocuments(expression);

        }


        public bool Any(Expression<Func<T, bool>> expression)
        {

            return this.Count(expression) > 0;

        }

        public T Get(string id)
        {
            return this.Collection.Find<T>(i => i.id == id).FirstOrDefault();
        }


        public T Create(T item)
        {
            this.Collection.InsertOne(item);
            return item;
        }

        public void Update(string id, T item)
        {
          this.Collection.ReplaceOne(i => i.id == id, item);
        }

        public void Remove(Expression<Func<T, bool>> expression)
        {
            this.Collection.DeleteMany(expression);
        }

        public void Remove(T item) { 
            this.Collection.DeleteOne(i => i.id == item.id);
        }

        public void Remove(string id) =>
            this.Collection.DeleteOne(i => i.id == id);
    }
}
