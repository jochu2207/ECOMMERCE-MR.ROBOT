using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {

        private CD_Categoria oCategory = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return oCategory.Listar();
        }


        public int Registrar(Categoria cat, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(cat.Descripcion) || string.IsNullOrWhiteSpace(cat.Descripcion))
            {
                Mensaje = "Por favor, ingresa una Categoria";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return oCategory.RegistrarCategoria(cat, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Categoria cat, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(cat.Descripcion) || string.IsNullOrWhiteSpace(cat.Descripcion))
            {
                Mensaje = "Por favor, ingresa una Categoria";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return oCategory.EditarCategoria(cat, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return oCategory.EliminarCategoria(id, out Mensaje);
        }

    }
}
