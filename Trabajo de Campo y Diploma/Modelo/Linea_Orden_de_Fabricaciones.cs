using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    internal class Linea_Orden_de_Fabricaciones
    {
        [Key]
        public int LineaOrdenFabricacionId { get; set; }

        [Required]
        public int OrdenFabricacionId { get; set; }

        [ForeignKey("OrdenFabricacionId")]
        public virtual Orden_de_Fabricaciones OrdenFabricacion { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal Cantidad { get; set; }
    }
}
