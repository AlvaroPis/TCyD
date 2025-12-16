using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_de_Campo_y_Diploma.Controladora.Seguridad;
using Trabajo_de_Campo_y_Diploma.Modelo;

namespace Trabajo_de_Campo_y_Diploma.Controladora.Seguridad
{
    public class Formulario
    {
        private static Formulario formulario;

        public static Formulario Obtener_instancia()
        {
            if (formulario == null)
            {
                formulario = new Formulario();
            }
            return formulario;
        }
        private Formulario() { }

        public List<Modelo.Formularios> getFormularios()
        {
            var formularios = from formulario in Modelo.Contexto.Obtener_instancia().Formularios
                              select formulario;
            return formularios.ToList();
        }

        public List<Modelo.Formularios> getFormularioId(int id)
        {
            var formularios = from formulario in Modelo.Contexto.Obtener_instancia().Formularios
                              where formulario.id_formulario == id
                              select formulario;
            return formularios.ToList();
        }
    }
}
