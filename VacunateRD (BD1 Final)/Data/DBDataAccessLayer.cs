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



        //Nueva Persona
        public void AddPersona(Persona p)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();


            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertPersona", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTipoIdentificacion", p.idTipoIdentificacion);
                cmd.Parameters.AddWithValue("@IdProfesion", p.idProfesion);
                cmd.Parameters.AddWithValue("@IdFase", p.idProfesion);
                cmd.Parameters.AddWithValue("@Nombres", p.nombres);
                cmd.Parameters.AddWithValue("@Apellidos", p.apellidos);
                cmd.Parameters.AddWithValue("@Identificacion", p.identificacion);
                cmd.Parameters.AddWithValue("@FechaNacimiento", p.FechaNacimiento);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
