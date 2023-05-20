using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }

        //***Categoria***\\
        #region
        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> olist = new List<Categoria>();
            olist = new CN_Categoria().Listar();
            return Json(new {data = olist}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarCategoria(Categoria cat)
        {
            object resultado;
            string mensaje = string.Empty;

            if (cat.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(cat, out mensaje);

            }
            else
            {
                resultado = new CN_Categoria().Editar(cat, out mensaje);
            }
            return Json(new {resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool rpta = false;
            string mensaje = string.Empty;

            rpta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new {result = rpta, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }
        #endregion


        //***MARCA***\\
        #region
        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarMarca(Marca obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if(obj.IdMarca == 0)
            {
                resultado = new CN_Marca().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(obj, out mensaje);
            }
            return Json(new {resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out mensaje);  

            return Json(new {resultado = respuesta, mensaje = mensaje}, JsonRequestBehavior.AllowGet);  
        }

        #endregion


        //***PRODUCTO***\\
        #region

        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oProd = new List<Producto>();
            oProd = new CN_Producto().Listar();
            return Json(new { data = oProd }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult RegistrarProducto(string obj, HttpPostedFileBase fileImg)
        {
            
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool save_image_success = true;

            Producto oProduct = new Producto();
            oProduct = JsonConvert.DeserializeObject<Producto>(obj);

            decimal precio;

            if(decimal.TryParse(oProduct.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oProduct.Precio = precio;
            }
            else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##"}, JsonRequestBehavior.AllowGet);
            }

            if (oProduct.IdProducto == 0)
            {
                int idProductoGen = new CN_Producto().Registrar(oProduct, out mensaje);

                if (idProductoGen != 0)
                {
                    oProduct.IdProducto = idProductoGen;
                }
                else
                {
                    operacion_exitosa = false;
                }

            }
            else
            {
                operacion_exitosa = new CN_Producto().Editar(oProduct, out mensaje);
            }

            if (operacion_exitosa)
            {
                if(fileImg != null)
                {

                    string rute_save = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(fileImg.FileName);
                    string name_img = string.Concat(oProduct.IdProducto.ToString(), extension);

                    try
                    {
                        fileImg.SaveAs(Path.Combine(rute_save, name_img));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        save_image_success = false;

                    }

                    if (save_image_success)
                    {
                        oProduct.RutaImagen = rute_save;
                        oProduct.NombreImagen = name_img;
                        bool rpta = new CN_Producto().SaveDataImg(oProduct, out mensaje);
                    }
                    else
                    {
                        mensaje = "Producto registrado, pero hubo problemas al gurdar la imagen";
                    }
                }
            }

            return Json(new {operacionExitosa = operacion_exitosa, idGenerado = oProduct.IdProducto, mensaje = mensaje}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ImgProduct(int id)
        {

            bool conversion;
            Producto oProd = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();

            string textBase64 = CN_Recursos.ConvertBase64(Path.Combine(oProd.RutaImagen,oProd.NombreImagen), out conversion);

            return Json(new
            {
                conversion = conversion,
                textBase64 = textBase64,
                extension = Path.GetExtension(oProd.NombreImagen)
            },
                JsonRequestBehavior.AllowGet
            );

        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool rpta = false;
            string mensaje = string.Empty;

            rpta = new CN_Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = rpta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}