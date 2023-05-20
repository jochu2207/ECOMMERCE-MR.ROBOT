using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Report
    {

        private CD_Report objCapaDato = new CD_Report();

        public List<Report> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objCapaDato.Ventas(fechainicio, fechafin, idtransaccion);
        }

        public DashBoard VerDashBoard()
        {
            return objCapaDato.VerDashBoard();
        }



    }
}
