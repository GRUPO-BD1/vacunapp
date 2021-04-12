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
                con.Close();
            }
            return personas;
        }
        public List<Vacunador> getVacunadoresByCentro(int idCentro)
        {
            List<Vacunador> vacunadores = new List<Vacunador>();
            Vacunador v1 = new Vacunador();

            using (SqlConnection con = new SqlConnection(s))
            {

                SqlCommand cmd = new SqlCommand("spSelectVacunadorByIdCentro", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCentro", idCentro);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    v1.Nombres = rdr["NOMBRES"].ToString();
                    v1.Apellidos = rdr["APELLIDOS"].ToString();
                    v1.IdVacunador = Convert.ToInt32(rdr["IDVACUNADOR"]);

                    vacunadores.Add(v1);
                }
                con.Close();
            }
            return vacunadores;
        }

        public List<Lote> getLotesByCentro(int idCentro)
        {
            List<Lote> lotes = new List<Lote>();
            Lote L1 = new Lote();

            using (SqlConnection con = new SqlConnection(s))
            {

                SqlCommand cmd = new SqlCommand("GetLoteByIdCentro", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCentro", idCentro);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    L1.Nombre = rdr["NOMBRE"].ToString();
                    L1.idLote = Convert.ToInt32(rdr["IDLOTE"]);
                    L1.Disponibles = Convert.ToInt32(rdr["DISPONIBLES"]);

                    lotes.Add(L1);
                }
                con.Close();
            }
            return lotes;
        }

        public List<MesaVacunacion> getMesaVacunacionByCentroID(int idCentro)
        {
            string query = "select IDMESAVACUNACION from TBL_MESAVACUNACION where IDCENTROVACUNACION = @idCentro";
            List<MesaVacunacion> Mesas = new List<MesaVacunacion>();
            MesaVacunacion m1 = new MesaVacunacion();
            using (SqlConnection con = new SqlConnection(s))
            {
                SqlCommand CMD = new SqlCommand(query, con);
                CMD.Parameters.AddWithValue("@idCentro", idCentro);
                con.Open();
                SqlDataReader rdr = CMD.ExecuteReader();
                while (rdr.Read())
                {
                    m1.idMesaVacunacion = Convert.ToInt32(rdr["IDMESAVACUNACION"]);
                    Mesas.Add(m1);
                }
                con.Close();
            }
            return Mesas;
        }
    }
}