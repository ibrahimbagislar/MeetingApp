using MeetingApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T?> GetAll(string jsonFilePath);
        T? GetById(string id, string jsonFilePath);
        T? GetByFilter(Expression<Func<T,bool>> filter, string jsonFilePath);
        void Create(T entity, string jsonFilePath);
        void Update(T entity, string jsonFilePath);
        void Remove(string id, string jsonFilePath);
    }
}
