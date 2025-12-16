using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public class Linea_Pedidos
    {
        [Key]
        public int LineaPedidoId { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public virtual Pedidos Pedido { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Precio { get; set; }
    }
}
