using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Categoria
    {

        public List<Categoria> Listar()
        {
            List<Categoria> list = new List<Categoria>();

            try
            {
                using (SqlConnection xconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "sp_ListCategoria";

                    SqlCommand cmd = new SqlCommand(query, xconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;

                    xconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }


            }
            catch(Exception)
            {

                list = new List<Categoria>();

            }
            return list;
        }

        public int RegistrarCategoria(Categoria cat, out string Mensaje)
        {
            int idAutogen = 0;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection xconexion = new SqlConnection(Conexion.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", xconexion);
                    cmd.Parameters.AddWithValue("Descripcion", cat.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", cat.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    xconexion.Open();

                    cmd.ExecuteNonQuery();
                    idAutogen = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    

                }


            }
            catch(Exception ex)
            {
                idAutogen = 0;
                Mensaje = ex.Message;
            }
            return idAutogen;
        }


        public bool EditarCategoria(Categoria cat, out string Mensaje)
        {
            bool result = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection xconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", xconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", cat.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", cat.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", cat.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    xconexion.Open();

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }


            }
            catch(Exception ex)
            {

                result = false;
                Mensaje = ex.Message;

            }
            return result;
        }

        public bool EliminarCategoria(int id, out string Mensaje)
        {

            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using(SqlConnection xconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", xconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    xconexion.Open();

                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }


            }
            catch(Exception ex)
            {
                resultado = false;
                Mensaje =ex.Message;    

            }
            return resultado;
        }


    }
}
