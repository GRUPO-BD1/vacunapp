using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace VacunateRD__BD1_Final_.Data
{
    public class DBDataAccessLayer
    {

        string s = "Server=tcp:vacunacion.database.windows.net,1433;Database=vacunacion_db;User ID=vacunacionadmin@vacunacion.database.windows.net;Password=Porfavor1;Trusted_Connection=False;Encrypt=True;";
        //Nueva Persona
        public void AddPersona(Persona p)
        {
            using (SqlConnection con = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spInsertPersona", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTipoIdentificacion", p.idTipoIdentificacion);
                cmd.Parameters.AddWithValue("@IdProfesion", p.idProfesion);
                cmd.Parameters.AddWithValue("@IdFase", p.idFase);
                cmd.Parameters.AddWithValue("@Nombres", p.nombres);
                cmd.Parameters.AddWithValue("@Apellidos", p.apellidos);
                cmd.Parameters.AddWithValue("@Identificacion", p.identificacion);
                cmd.Parameters.AddWithValue("@FechaNacimiento", p.FechaNacimiento);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public List<PersonaVacunacion> GetPersonaData(string id)
        {
            List<PersonaVacunacion> personas = new List<PersonaVacunacion>();
            PersonaVacunacion p1 = new PersonaVacunacion();

            using (SqlConnection con = new SqlConnection(s))
            {

                SqlCommand cmd = new SqlCommand("spSelectVacunacionByIdentificacionPersona", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Identificacion", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    p1.Nombres = rdr["NOMBRES"].ToString();
                    p1.Apelllidos = rdr["APELLIDOS"].ToString();
                    p1.Identificacion = rdr["IDENTIFICACION"].ToString();
                    p1.Fechanacimiento = Convert.ToDateTime(rdr["FECHANACIMIENTO"]);
                    p1.Profesion = rdr["PROFESION"].ToString();
                    p1.CodigoFase = rdr["CODIGOFASE"].ToString();
                    p1.NumMesa = rdr["NUMMESA"].ToString();
                    p1.Centro = rdr["CENTRO"].ToString();
                    p1.Fechaproporcionada = Convert.ToDateTime(rdr["FECHAPROPORCIONADA"]);
                    p1.IdLote = Convert.ToInt32(rdr["IDLOTE"]);
                    p1.DosisCorrespondiente = Convert.ToInt32(rdr["DOSISCORRESPONDIENTE"]);
                    p1.NombreVacuna = rdr["NOMBREVACUNA"].ToString();
                    personas.Add(p1);
                }
            }
            return personas;
        }


    }
}
