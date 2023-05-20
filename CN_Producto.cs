using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto
    {
       private CD_Producto oCapData = new CD_Producto();

        public List<Producto> Listar()
        {
            return oCapData.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "Por favor, ingresa el nombre al producto";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Por favor, ingresa la descripcion del producto";
            }
            else if (obj.oMarcar.IdMarca == 0)
            {
                Mensaje = "Por favor, selecciona una Marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Por favor, selecciona una Categoria";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Por favor, ingresa el precio del Producto";
            }
            else if(obj.Stock == 0)
            {
                Mensaje = "Por favor, ingresa el stock del Producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return oCapData.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }


        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "Por favor, ingresa el nombre del Producto";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Por favor, ingresa la descripcion del producto";
            }
            else if (obj.oMarcar.IdMarca == 0)
            {
                Mensaje = "Por favor, selecciona una Marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Por favor, selecciona una Categoria";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Por favor, ingresa el precio del Producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Por favor, ingresa el stock del Producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return oCapData.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }


        public bool SaveDataImg(Producto obj, out string Mensaje)
        {
            return oCapData.SaveDataImg(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return oCapData.Eliminar(id, out Mensaje);
        }

    }
}
