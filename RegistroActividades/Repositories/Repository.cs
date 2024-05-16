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
        // crea un repositorio generico en sqlite de tipo T con las operaciones basicas usando sqlite-net-pcl 
        private SQLiteAsyncConnection connection;
        //public Repository()
        //{
        //    connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RegistroActividades.db3"));
        //    connection.CreateTableAsync<T>().Wait();
        //}

        //public async Task<List<T>> GetAll()
        //{
        //    return await connection.Table<T>().ToListAsync();
        //}

        //public async Task<T> GetById(int id)
        //{
        //    return await connection.FindAsync<T>(id);
        //}

        public async Task<int> Insert(T entity)
        {
            return await connection.InsertAsync(entity);
        }

        public async Task<int> Update(T entity)
        {
            return await connection.UpdateAsync(entity);
        }
        
        public async Task<int> Delete(T entity)
        {
            return await connection.DeleteAsync(entity);
        }

        public async Task<int> DeleteAll()
        {
            return await connection.DeleteAllAsync<T>();
        }

    }
}
