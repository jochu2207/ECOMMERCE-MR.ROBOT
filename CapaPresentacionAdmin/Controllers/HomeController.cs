using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> olista = new List<Usuario>();

            olista = new CN_Usuario().Listar();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult RegistrarUsuario(Usuario obj)
        {

            object result;
            string mensaje = string.Empty;

            if (obj.IdUsuario == 0)
            {
                result = new CN_Usuario().Registrar(obj, out mensaje);
            }
            else
            {
                result = new CN_Usuario().Editar(obj, out mensaje);
            }

            return Json(new { resultado = result, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {

            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuario().Eliminar(id, out mensaje);

            return Json(new {resultado = respuesta, mensaje = mensaje}, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListReport(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Report> oList = new List<Report>();

            oList = new CN_Report().Ventas(fechainicio, fechafin, idtransaccion);

            return Json(new {data = oList }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ViewDashBoard()
        {
            DashBoard obj = new CN_Report().VerDashBoard();

            return Json(new { resultado = obj }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public FileResult ExportVenta(string fechainicio, string fechafin, string idtransaccion)
        {

            List<Report> oList = new List<Report>();

            oList = new CN_Report().Ventas(fechainicio, fechafin, idtransaccion);

            DataTable dt = new DataTable();

            dt.Locale = new CultureInfo("es-PE");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("IdTransaccion", typeof(string));

            foreach(Report rp in oList)
            {
                dt.Rows.Add(new object[]
                {
                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Producto,
                    rp.Precio,
                    rp.Cantidad,
                    rp.Total,
                    rp.IdTransaccion
                });
            }

            dt.TableName = "Datos";

            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using(MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }

        }

    }
}