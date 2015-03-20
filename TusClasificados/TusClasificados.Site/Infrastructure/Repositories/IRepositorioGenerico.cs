using System;
namespace TusClasificados.Site.Infrastructure
{
    public interface IRepositorioGenerico<T> where T : class
    {
        void Delete(object id);
        void Insert(T obj);
        void Save();
        System.Collections.Generic.IEnumerable<T> SelectAll();
        T SelectById(object id);
        void Update(T obj);
    }
}
