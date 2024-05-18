using RegistroDeActividades.Models.Entities;
using SQLite;
using System.IO;

namespace RegistroActividades.Repositories
{
    public class DepartamentosRepository
    {

        SQLiteConnection context;
        public DepartamentosRepository()
        {
            string ruta = Path.Combine(Environment.CurrentDirectory + "/departamentos.db3");
            context = new(ruta);
            context.CreateTable<Departamentos>();
        }

        public void Insert(Departamentos departamento)
        {
            context.Insert(departamento);
        }

        public void Update(Departamentos departamento)
        {
            context.Update(departamento);
        }

        public void InsertOrReplace(Departamentos departamento)
        {
            context.InsertOrReplace(departamento);
        }

        public void Delete(Departamentos departamento)
        {
            context.Delete(departamento);
        }

        public IEnumerable<Departamentos> GetAll()
        {
            return context.Table<Departamentos>().OrderBy(x => x.Nombre);
        }
        public Departamentos Get(int id)
        {
            return context.Find<Departamentos>(id);
        }


    }
}
