using System.Collections.Generic;

namespace Merial.PetPixie.Core.Repository
{
    public interface IRepository<T>
    {
        T GetById(string id);

        IEnumerable<T> GetAll();

        int Insert(T item);

        int Update(T item);

        int Delete(T item);
    }
}