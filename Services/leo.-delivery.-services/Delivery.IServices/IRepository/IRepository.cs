using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IRepository
{
    public interface IRepository<T> : IDisposable
            where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void CreateList(List<T> itemList);
        void Update(T item);
        void UpdateList(List<T> itemList);
        void Delete(int id);
        void DeleteList(List<T> itemList);
        void Save();
    }
}
