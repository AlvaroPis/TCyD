using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public partial class Formularios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Formularios()
        {
            Permisos = new HashSet<Permisos>(); // Correcto: inicializa la colección
        }

        [Key]
        public int id_formulario { get; set; } 

        [StringLength(30)]
        public string nombre { get; set; } 

        public int? id_modulo { get; set; } 

        public virtual Modulos Modulos { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permisos> Permisos { get; set; } // Relación muchos-a-muchos con Permisos
    }

}
