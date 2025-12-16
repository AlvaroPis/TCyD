using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public partial class Modulos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modulos()
        {
            Formularios = new HashSet<Formularios>(); // Correcto: inicialización de la colección
        }

        [Key]
        public int id_modulo { get; set; } // Correcto: clave primaria

        [StringLength(20)] // Limita el nombre a 20 caracteres
        public string nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Formularios> Formularios { get; set; } // Relación uno-a-muchos
    }
}
