using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public class Pedidos
    {
        [Key]
        public int PedidoId { get; set; }

        [Required]
        public int id_cliente { get; set; }

        [ForeignKey("id_cliente")]
        public virtual Clientes Cliente { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public virtual ICollection<Linea_Pedidos> LineaPedidos { get; set; } = new List<Linea_Pedidos>();
    }
}

