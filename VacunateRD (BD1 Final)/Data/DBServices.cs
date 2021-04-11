using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacunateRD__BD1_Final_.Data
{
    public class DBServices
    {
        DBDataAccessLayer dbAccess = new DBDataAccessLayer();
        public string Create(Persona p1)
        {
            //DBDataAccessLayer dbAccess = new DBDataAccessLayer();
            dbAccess.AddPersona(p1);
            return "Exito";
        }
        public List<PersonaVacunacion> GetPersonaDataByIdentificacion(string id)
        {
            //DBDataAccessLayer dbAccess = new DBDataAccessLayer();
            return dbAccess.GetPersonaData(id);
            
        }
    }
}
