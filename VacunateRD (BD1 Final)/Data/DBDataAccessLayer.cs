using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VacunateRD__BD1_Final_.Data.Data_models.VacunadosPorArea;
using VacunateRD__BD1_Final_.Data.Data_models.VacunasDisponibles;

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
        private List<VacunadosPorRegion> GetVacunadosPorRegion()
        {
            List<VacunadosPorRegion> porRegion = new List<VacunadosPorRegion>();
            using (SqlConnection con = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spCantidadVacunacionesPorRegion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.WriteLine(rdr["CANTIDADVACUNACIONES"]);
                    Console.WriteLine(rdr["REGION"]);
                    porRegion.Add(new VacunadosPorRegion(
                        rdr["REGION"].ToString(),
                        Convert.ToInt32(rdr["CANTIDADVACUNACIONES"])));
                }
            }
            return porRegion;
        }
        private List<VacunadosPorProvincia> GetVacunadosPorProvincia()
        {
            List<VacunadosPorProvincia> porProvincia = new List<VacunadosPorProvincia>();
            using (SqlConnection con = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spCantidadVacunacionesPorProvincia", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    porProvincia.Add(new VacunadosPorProvincia(
                        rdr["PROVINCIA"].ToString(),
                        Convert.ToInt32(rdr["CANTIDADVACUNACIONES"])));
                }
            }
            return porProvincia;
        }
        private List<VacunadosPorMunicipio> GetVacunadosPorMunicipio()
        {
            List<VacunadosPorMunicipio> porMunicipio = new List<VacunadosPorMunicipio>();
            using (SqlConnection con = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spCantidadVacunacionesPorMunicipio", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    porMunicipio.Add(new VacunadosPorMunicipio(
                        rdr["MUNICIPIO"].ToString(),
                        Convert.ToInt32(rdr["CANTIDADVACUNACIONES"])));
                }
            }
            return porMunicipio;
        }
        private List<VacunasDisponiblesPorRegion> GetVacunasDisponiblesPorRegion()
        {
            List<VacunasDisponiblesPorRegion> vacunasDisponiblesPorRegion = new List<VacunasDisponiblesPorRegion>();
            using (SqlConnection con = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spVacunasDisponiblesRegion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    vacunasDisponiblesPorRegion.Add(new VacunasDisponiblesPorRegion(
                        rdr["REGION"].ToString(),
                        Convert.ToInt32(rdr["DOSISDISPONIBLES"])));
                }
            }
            return vacunasDisponiblesPorRegion;
        }

        private int GetVacunasDisponiblesCentral()
        {
            using (SqlConnection con = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spVacunasDisponiblesCentral", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public DatosEstadisticos GetStatsData()
        {
            return new DatosEstadisticos(
                GetVacunadosPorRegion(),
                GetVacunadosPorProvincia(),
                GetVacunadosPorMunicipio(),
                GetVacunasDisponiblesPorRegion(),
                GetVacunasDisponiblesCentral());
        }
    }
}
