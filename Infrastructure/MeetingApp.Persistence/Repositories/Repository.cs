using MeetingApp.Application.Interfaces;
using MeetingApp.Application.Static;
using MeetingApp.Domain.Entities.Common;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace MeetingApp.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public void Create(T entity, string jsonFilePath)
        {
            // json verilerini listeye çevir
            var currentData = GetAll(jsonFilePath);

            // Eski verileri yeni verilerle birleştir
            currentData?.Add(entity);

            // Birleştirilmiş veriyi json formatına çevir
            string newData = JsonConvert.SerializeObject(currentData, Formatting.Indented);

            // ilgili json dosyasına yaz
            File.WriteAllText(jsonFilePath, newData);
        }

        public List<T?> GetAll(string jsonFilePath)
        {
            // jsonFilePath path'indeki dosyayı oku
            string jsonData = File.ReadAllText(jsonFilePath);
            // Verilen T modeline json datasını convert et
            var datas = JsonConvert.DeserializeObject<List<T>>(jsonData);
            return datas;
        }

        public T? GetByFilter(Expression<Func<T, bool>> filter, string jsonFilePath)
        {
            var datas = GetAll(jsonFilePath);
            return datas.AsQueryable().SingleOrDefault(filter);
        }

        public T? GetById(string id, string jsonFilePath)
        {
            //tüm verileri çek
            var datas = GetAll(jsonFilePath);
            // veri ne olursa olsun geri dönüş türü Base Entity'den kalıtılmış olacağı için oradaki Id değerini kullanabiliyoruz
            return datas.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(string id, string jsonFilePath)
        {
            var currentData = GetAll(jsonFilePath);
            var removedEntity = currentData.FirstOrDefault(x => x.Id == id);
            if (removedEntity != null)
            {
                currentData.Remove(removedEntity);
                string newData = JsonConvert.SerializeObject(currentData, Formatting.Indented);
                File.WriteAllText(jsonFilePath, newData);
            }
        }

        public void Update(T entity, string jsonFilePath)
        {
            var currentData = GetAll(jsonFilePath);
            var updatedEntity = currentData.FirstOrDefault(x => x.Id == entity.Id);
            if (updatedEntity != null)
            {
                currentData.Remove(updatedEntity);
                currentData.Add(entity);
                string newData = JsonConvert.SerializeObject(currentData, Formatting.Indented);
                File.WriteAllText(jsonFilePath,newData);
            }
        }
    }
}
