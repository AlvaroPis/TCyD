using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public partial class Clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clientes()
        {
            
        }

        [Key]
        public int id_cliente { get; set; }

        public int? dni { get; set; }

        [StringLength(60)]
        public string nombre { get; set; }

        [StringLength(60)]
        public string email { get; set; }

        [StringLength(20)]
        public string telefono { get; set; }

        public bool? estado { get; set; }
    }
}
