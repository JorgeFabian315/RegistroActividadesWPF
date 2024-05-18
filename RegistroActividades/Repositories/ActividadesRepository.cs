using RegistroDeActividades.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            string ruta = Path.Combine( Environment.CurrentDirectory +"/actividades.db3");
            context = new(ruta);
            context.CreateTable<Actividades>();
        }

        public void Insert(Actividades actividad)
        {
            context.Insert(actividad);
        }

        public void Update(Actividades actividad)
        {
            context.Update(actividad);
        }
        public void InsertOrReplace(Actividades actividad)
        {
            context.InsertOrReplace(actividad);
        }
        public void Delete(Actividades actividad)
        {
            context.Delete(actividad);
        }

        public IEnumerable<Actividades> GetAll()
        {
            return context.Table<Actividades>().OrderBy(x => x.Titulo);
        }
        public Actividades Get(int id)
        {
            return context.Find<Actividades>(id);
        }












    }
}
