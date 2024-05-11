using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace RegistroActividades.Repositories
{
    public class Repository<T> where T: class
    {
        //SQLiteConnection context;
        //public Repository(string ruta)
        //{
        //    context = new(ruta);
        //    context.CreateTable<T>();
        //}

        //public void Insert(T libro)
        //{
        //    context.Insert(libro);
        //}

        //public void Update(T libro)
        //{
        //    context.Update(libro);
        //}
        //public void InsertOrReplace(T libro)
        //{
        //    context.InsertOrReplace(libro);
        //}
        //public void Delete(T libro)
        //{
        //    context.Delete(libro);
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return context.Table<T>().ToList();
        //}
        //public T Get(int id)
        //{
        //    return context.Find<T>(id);
        //}

    }
}
