using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public partial class Productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productos()
        {
            Linea_Productos = new HashSet<Linea_Productos>();
        }

        [Key]
        public int ProductoId { get; set; }

        [StringLength(60)]
        public string descripcion { get; set; }

        public int? stock { get; set; }

        public decimal? costo_producto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<Linea_Productos> Linea_Productos { get; set; }
    }
}
