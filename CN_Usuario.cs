using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {

        private CDUsuarios oCapaDato = new CDUsuarios();

        public List<Usuario> Listar()
        {
            return oCapaDato.Listar();
        }


        public int Registrar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Por favor, ingresa tus nombres";
            }

            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Por favor, ingresa tus apellidos";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Por favor, ingresa un correo";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();
                string asunto = "Registro de cuenta";
                string msg_mail = "<h3>La cuenta ha sido creada satisfactoriamente</h3></br><p>La contraseña de acceso es: !clave!</p>";
                msg_mail = msg_mail.Replace("!clave!", clave);

                bool rpta = CN_Recursos.SendMail(obj.Correo, asunto, msg_mail);

                if (rpta)
                {
                    obj.Clave = CN_Recursos.ConvertToSha256(clave);
                    return oCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se pudo enviar el Correo";
                    return 0;
                }

            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Este campo no puede ser vacio";

            }
            else if(string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Este campo no puede ser vacio";

            }
            else if(string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Este campo no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return oCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return oCapaDato.Eliminar(id, out Mensaje);
        }







    }
}
