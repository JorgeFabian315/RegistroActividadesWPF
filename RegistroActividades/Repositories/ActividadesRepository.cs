using RegistroDeActividades.Models.Entities;
using SQLite;
using System.IO;
using System.Threading.Tasks;

namespace RegistroActividades.Repositories
{
    public class ActividadesRepository
    {
        //Creame un reositorio para las actividades en sqlite con las operaciones basicas
        // CRUD

        SQLiteConnection context;
        public ActividadesRepository()
        {
            string ruta = Path.Combine(Environment.CurrentDirectory + "/actividades.db3");
            context = new(ruta);
            context.CreateTable<Actividades>();
        }

        public void Insert(Actividades actividad)
        {
            context.Insert(actividad);
        }

        public int Update(Actividades actividad)
        {
            return context.Update(actividad);
        }
        public int Delete(Actividades actividad)
        {
            return context.Delete(actividad);
        }

        public List<Actividades> GetAll()
        {
            return context.Table<Actividades>().OrderBy(x => x.Titulo).ThenBy(x => x.FechaActualizacion).ToList();
        }
        public Actividades Get(int id)
        {
            return context.Find<Actividades>(id);
        }

        public void DeleteAll()
        {
            context.DeleteAll<Actividades>();
        }









    }
}
