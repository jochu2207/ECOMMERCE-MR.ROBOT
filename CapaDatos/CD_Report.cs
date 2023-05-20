using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Report
    {

        public List<Report> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Report> list = new List<Report>();

            try
            {
                using (SqlConnection oConex = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReportVentas", oConex);
                    cmd.Parameters.AddWithValue("fechInicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechafin);
                    cmd.Parameters.AddWithValue("idTransac", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConex.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Report()
                            {
                                FechaVenta = dr["FechaVenta"].ToString(),
                                Cliente = dr["Cliente"].ToString(),
                                Producto = dr["Producto"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"],new CultureInfo("es-PE")),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-PE")),
                                IdTransaccion = dr["IdTransaccion"].ToString()

                            });
                        }
                    }

                }


            }
            catch
            {
                list = new List<Report>();
            }
            return list;
        }

        public DashBoard VerDashBoard()
        {
            DashBoard obj = new DashBoard();

            try
            {

                using (SqlConnection oConex = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReportDashboard", oConex);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConex.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            obj = new DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"])
                            };

                        }
                    }

                }

            }
            catch
            {
                obj = new DashBoard();
            }

            return obj;
        }


    }
}
