using LiteDB;
using Simple.Data.Models;

namespace Simple.Api.Services
{
    public interface IGenericService
    {
        void SaveInLiteDb<T>(T entity, string dbCollection);
        List<GenericItem<T>> GetFromLiteDb<T>(int[] ids, string dbCollection);
    }

    public class GenericService : IGenericService
    {
        string connectionString = Directory.GetCurrentDirectory() + "\\LiteDb\\SimpleData.db";

        public List<GenericItem<T>> GetFromLiteDb<T>(int[] ids, string dbCollection)
        {
            using (var db = new LiteDatabase(this.connectionString))
            {
                List<GenericItem<T>> col = new List<GenericItem<T>>();
                if(ids.Count() == 0)
                {
                    col = db.GetCollection<GenericItem<T>>(dbCollection).Query().ToList();
                }
                else
                {
                    // Get a collection (or create, if doesn't exist)
                    col = db.GetCollection<GenericItem<T>>(dbCollection).Query()
                        .Where(x => ids.Contains(x.Id))
                        .ToList();
                }

                return col;
            }
        }

        public void SaveInLiteDb<T>(T entity, string dbCollection)
        {
            var item = new GenericItem<T>();
            item.Body = entity;
            using (var db = new LiteDatabase(this.connectionString))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<GenericItem<T>>(dbCollection);

                col.Insert(item);
            }
        }
    }
}
