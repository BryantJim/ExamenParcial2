using System;
using System.Collections.Generic;
using System.Text;
using ExamenParcial.Entidades;
using ExamenParcial.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace ExamenParcial.BLL
{
    public class LlamadasBLL
    {
        public static bool Guardar(Llamadas llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Llamadas.Add(llamada) != null)
                    paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Llamadas llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM LlamadaDetalle Where LlamadaId = {llamada.LlamadaId}");
                foreach(var item in llamada.LlamadasDetalle)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(llamada).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Eliminar = LlamadasBLL.Buscar(id);
                db.Entry(Eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Llamadas Buscar(int id)
        {
            Llamadas llamada = new Llamadas();
            Contexto db = new Contexto();

            try
            {
                llamada = db.Llamadas.Include(x => x.LlamadasDetalle).Where(x => x.LlamadaId == id).SingleOrDefault();   
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return llamada;
        }

        public static List<Llamadas> GetList(Expression<Func<Llamadas, bool>> llamada)
        {
            List<Llamadas> Lista = new List<Llamadas>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Llamadas.Where(llamada).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}
