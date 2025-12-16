using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_de_Campo_y_Diploma.Modelo;


namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public partial class Permisos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permisos()
        {
            Usuarios = new HashSet<Usuarios>();
            Grupos = new HashSet<Grupos>();
        }

        [Key]
        public int id_permiso { get; set; }

        [StringLength(50)]
        public string nombre_permiso { get; set; }

        public int? id_formulario { get; set; }

        public bool? estado { get; set; }
        public virtual Formularios Formularios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grupos> Grupos { get; set; }
    }
}
