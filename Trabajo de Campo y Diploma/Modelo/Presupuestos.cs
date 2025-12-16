using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public partial class Presupuestos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Presupuestos()
        {
            Linea_Presupuestos = new HashSet<Linea_Presupuestos>();
        }

        [Key]
        public int PresupuestoId { get; set; }

        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Clientes Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Linea_Presupuestos> Linea_Presupuestos { get; set; }
    }
}
