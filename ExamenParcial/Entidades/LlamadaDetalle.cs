using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExamenParcial.Entidades
{
    public class LlamadaDetalle
    {
        [Key]
        public int LlamadaId { get; set; }
        public String Problema { get; set; }
        public String Solucion { get; set; }

        public LlamadaDetalle()
        {
            LlamadaId = 0;
            Problema = string.Empty;
            Solucion = string.Empty;
        }

        public LlamadaDetalle(int llamadaId, string problema, string solucion)
        {
            LlamadaId = llamadaId;
            Problema = problema;
            Solucion = solucion;
        }
    }
}
