﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Marca
    {

        private CD_Marca oCapData = new CD_Marca();

        public List<Marca> Listar()
        {
            return oCapData.Listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Por favor, escribe una descripcion valida";
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

        public bool Editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Por favor, escribe una descripcion valida";
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


        public bool Eliminar(int id, out string Mensaje)
        {
            return oCapData.Eliminar(id, out Mensaje);
        }
        public List<Marca> ListarMarcaPorCategoria(int idCategoria)
        {
            return oCapData.ListarMarcaPorCategoria(idCategoria);
        }

    }
}
