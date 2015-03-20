using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TusClasificados.Site.Models;

namespace TusClasificados.Site.Infrastructure
{
    public class RepositorioGenerico<T> : TusClasificados.Site.Infrastructure.IRepositorioGenerico<T> where T : class
    {
        private ApplicationDbContext contexto;
        private DbSet<T> tabla = null;

        public RepositorioGenerico(ApplicationDbContext context)
        {
            this.contexto = context;
            tabla = contexto.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return tabla.ToList();
        }

        public T SelectById(object id)
        {
            return tabla.Find(id);
        }

        public void Insert(T obj)
        {
            tabla.Add(obj);
        }

        public void Update(T obj)
        {
            tabla.Attach(obj);
            contexto.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T obj = tabla.Find(id);
            tabla.Remove(obj);
        }

        public void Save()
        {
            contexto.SaveChanges();
        }
    }
}