using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacunateRD__BD1_Final_.Data
{
    public class CentroVacunacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public CentroVacunacion(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}
